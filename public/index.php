<?php
declare(strict_types=1);

spl_autoload_register(static function(string $name): void {
    $prefix = 'Northfield\\';
    if (!str_starts_with($name, $prefix)) return;
    $class = substr($name, strlen($prefix));
    $path = dirname(__DIR__) . '/app/' . str_replace('\\', '/', $class) . '.php';
    if (is_file($path)) require_once $path;
});

use Northfield\Fiscal\PgdasCalculator;
use Northfield\Http\Http;

header('X-Content-Type-Options: nosniff');
header('Referrer-Policy: no-referrer');
header('X-Frame-Options: DENY');
header('Permissions-Policy: camera=(), microphone=(), geolocation=()');
header("Content-Security-Policy: default-src 'self'; script-src 'self'; style-src 'self'; img-src 'self' data:; font-src 'self'; object-src 'none'; base-uri 'self'; frame-ancestors 'none'; form-action 'self'; connect-src 'self'");

$path = parse_url($_SERVER['REQUEST_URI'], PHP_URL_PATH) ?: '/';
$calc = new PgdasCalculator(dirname(__DIR__) . '/fiscal-rules/2026/anexo-i.json');

if ($path === '/api/calcular') {
    Http::headers();
    if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
        header('Allow: POST'); Http::json(405, ['ok' => false, 'erro' => 'Método não permitido.']);
    }
    if (!str_starts_with(strtolower($_SERVER['CONTENT_TYPE'] ?? ''), 'application/json')) {
        Http::json(415, ['ok' => false, 'erro' => 'Envie application/json.']);
    }
    $origin = $_SERVER['HTTP_ORIGIN'] ?? '';
    if ($origin !== '') {
        $url = parse_url($origin);
        $expectedHost = $_SERVER['HTTP_HOST'] ?? '';
        $originHost = ($url['host'] ?? '') . (isset($url['port']) ? ':' . $url['port'] : '');
        $expectedScheme = (!empty($_SERVER['HTTPS']) && $_SERVER['HTTPS'] !== 'off') ? 'https' : 'http';
        if ($originHost !== $expectedHost || ($url['scheme'] ?? '') !== $expectedScheme) {
            Http::json(403, ['ok' => false, 'erro' => 'Origem não autorizada.']);
        }
    }
    if (in_array($_SERVER['HTTP_SEC_FETCH_SITE'] ?? '', ['cross-site'], true)) {
        Http::json(403, ['ok' => false, 'erro' => 'Requisição de outra origem bloqueada.']);
    }
    $length = (int)($_SERVER['CONTENT_LENGTH'] ?? 0);
    if ($length > 8192) Http::json(413, ['ok' => false, 'erro' => 'Requisição excedeu o limite de 8 KB.']);
    $raw = file_get_contents('php://input', false, null, 0, 8193);
    if ($raw === false || strlen($raw) > 8192) Http::json(413, ['ok' => false, 'erro' => 'Payload muito grande.']);
    try {
        $data = json_decode($raw, true, 64, JSON_THROW_ON_ERROR);
        if (!is_array($data) || array_is_list($data)) throw new \DomainException('Objeto JSON inválido.');
        $allowed = ['competencia','anexo','regime','receita_mensal','rbt12','operacao_comum','mais_de_12_meses','sem_situacoes_especiais'];
        $unknown = array_diff(array_keys($data), $allowed);
        if ($unknown) throw new \DomainException('Campos inesperados: ' . implode(', ', array_slice($unknown, 0, 4)));
        $result = $calc->calculate($data);
        Http::json(200, ['ok' => true, 'resultado' => $result]);
    } catch (\InvalidArgumentException|\DomainException|\JsonException $e) {
        Http::json(422, ['ok' => false, 'erro' => $e->getMessage()]);
    } catch (\Throwable $e) {
        error_log('Northfield: erro técnico no cálculo; código ' . get_class($e));
        Http::json(500, ['ok' => false, 'erro' => 'Falha técnica no cálculo. Tente novamente mais tarde.']);
    }
}

if ($path === '/api/regras') {
    Http::headers();
    if ($_SERVER['REQUEST_METHOD'] !== 'GET') Http::json(405, ['ok' => false, 'erro' => 'Método não permitido.']);
    Http::json(200, ['ok' => true, 'regras' => $calc->rulesPublic()]);
}

if ($path !== '/') { http_response_code(404); $page = 'notfound'; }
else {
    $page = $_GET['page'] ?? 'painel';
    if (!is_string($page) || !in_array($page, ['painel','apuracao','historico','conferencia','regras','privacidade','sobre'], true)) {
        http_response_code(404); $page = 'notfound';
    }
}
header('Cache-Control: no-store, private, max-age=0');
require dirname(__DIR__) . '/resources/views/layout.php';
