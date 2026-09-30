<?php
declare(strict_types=1);

namespace Northfield\Http;

final class Http
{
    public static function headers(): void
    {
        header('Content-Type: application/json; charset=utf-8');
        header('Cache-Control: no-store, private, max-age=0');
        header('X-Content-Type-Options: nosniff');
        header('Referrer-Policy: no-referrer');
    }
    public static function json(int $status, array $data): never
    {
        http_response_code($status);
        echo json_encode($data, JSON_UNESCAPED_UNICODE | JSON_UNESCAPED_SLASHES | JSON_THROW_ON_ERROR);
        exit;
    }
}
