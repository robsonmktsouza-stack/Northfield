<?php
declare(strict_types=1);

namespace Northfield\Fiscal;

use Northfield\Core\BigNatural as B;
use Northfield\Core\Money;

final class PgdasCalculator
{
    /** @var array<string,mixed> */
    private array $rule;

    public function __construct(string $rulePath)
    {
        $content = @file_get_contents($rulePath);
        if ($content === false) throw new \RuntimeException('Catálogo de regras indisponível.');
        $this->rule = json_decode($content, true, 512, JSON_THROW_ON_ERROR);
    }

    /** @param array<string,mixed> $input @return array<string,mixed> */
    public function calculate(array $input): array
    {
        // Escopo explicitamente limitado. Casos especiais NÃO devem ser estimados como operações comuns.
        if (($input['competencia'] ?? null) === null || !is_string($input['competencia']) || !preg_match('/^2026-(0[1-9]|1[0-2])$/D', $input['competencia'])) {
            throw new \DomainException('Esta versão calcula apenas competências de 2026.');
        }
        if (($input['anexo'] ?? null) !== 'I') {
            throw new \DomainException('O MVP aceita apenas Anexo I (comércio).');
        }
        if (($input['regime'] ?? null) !== 'competencia') {
            throw new \DomainException('O regime de caixa ainda não é coberto pelo MVP.');
        }
        if (($input['operacao_comum'] ?? null) !== true || ($input['mais_de_12_meses'] ?? null) !== true || ($input['sem_situacoes_especiais'] ?? null) !== true) {
            throw new \DomainException('Confirme que é comércio comum, empresa com mais de 12 meses e sem situações especiais.');
        }
        $receita = Money::toCents($input['receita_mensal'] ?? null);
        $rbt = Money::toCents($input['rbt12'] ?? null);
        if (B::compare($rbt, '0') === 0) throw new \DomainException('RBT12 igual a zero exige análise específica de início de atividade.');
        if (B::compare($rbt, '360000000') > 0) throw new \DomainException('RBT12 acima de R$ 3.600.000,00 exige tratamento de sublimites ainda não implementado.');
        if (B::compare($receita, '480000000') > 0) throw new \DomainException('Receita mensal acima do limite de R$ 4.800.000,00 não é suportada.');

        $faixa = null;
        foreach ($this->rule['faixas'] as $item) {
            if (B::compare($rbt, $item['ate_centavos']) <= 0) { $faixa = $item; break; }
        }
        if ($faixa === null) throw new \DomainException('Faixa tributária não suportada.');
        $numerador = B::subtract(
            B::multiply($rbt, (string)$faixa['aliquota_bps']),
            B::multiply((string)$faixa['deducao_centavos'], '10000')
        );
        $denominador = B::multiply($rbt, '10000');
        $totalCents = B::divideRounded(B::multiply($receita, $numerador), $denominador);
        // Alíquota efetiva em percentual com 6 decimais, usada só para exibição.
        $effectiveMicroPct = B::divideRounded(B::multiply($numerador, '100000000'), $denominador);
        $padded = str_pad($effectiveMicroPct, 7, '0', STR_PAD_LEFT);
        $effectivePercentage = substr($padded, 0, -6) . '.' . substr($padded, -6);

        $shares = []; $sumCents = '0';
        foreach ($faixa['reparticao_bps'] as $tax => $basisPoints) {
            $cents = B::divideRounded(B::multiply($totalCents, (string)$basisPoints), '10000');
            $shares[] = [
                'tributo' => $tax,
                'participacao_percentual' => number_format($basisPoints / 100, 2, '.', ''),
                'valor' => Money::fromCents($cents),
            ];
            $sumCents = B::add($sumCents, $cents);
        }
        $diffNegative = B::compare($sumCents, $totalCents) > 0;
        $residuo = $diffNegative ? B::subtract($sumCents, $totalCents) : B::subtract($totalCents, $sumCents);

        return [
            'escopo' => 'simulacao_matematica_comercio_comum',
            'status' => 'NAO_HOMOLOGADO',
            'alerta' => 'Simulação para conferência. Não é PGDAS-D transmitido nem DAS oficial. A repartição individual pode diferir por arredondamento.',
            'competencia' => $input['competencia'],
            'anexo' => 'I',
            'receita_mensal' => Money::fromCents($receita),
            'rbt12' => Money::fromCents($rbt),
            'faixa' => $faixa['numero'],
            'aliquota_nominal_percentual' => number_format($faixa['aliquota_bps'] / 100, 2, '.', ''),
            'parcela_deduzir' => Money::fromCents((string)$faixa['deducao_centavos']),
            'aliquota_efetiva_percentual' => $effectivePercentage,
            'total_estimado' => Money::fromCents($totalCents),
            'reparticao_indicativa' => $shares,
            'diferenca_arredondamento' => ($diffNegative ? '-' : '') . Money::fromCents($residuo),
            'regra' => [
                'id' => $this->rule['id'],
                'versao' => $this->rule['versao'],
                'status' => $this->rule['status'],
                'referencias' => $this->rule['referencias'],
            ],
            'memoria' => [
                'formula' => 'Alíquota efetiva = ((RBT12 × alíquota nominal) − parcela a deduzir) ÷ RBT12',
                'calculo_mensal' => 'Receita mensal × alíquota efetiva',
                'reparticao' => 'Percentuais do Anexo I aplicados ao valor calculado, individualmente arredondados ao centavo.',
            ],
            'limitacoes' => [
                'Não cobre ICMS-ST, monofásico, exportação, devoluções, isenções ou redução de base.',
                'Não cobre regime de caixa, abertura recente, sublimites, atividades mistas ou excesso de limite.',
                'Não reproduz todas as regras de apuração, arredondamento e declaração do PGDAS-D oficial.',
            ],
        ];
    }

    public function rulesPublic(): array
    {
        return $this->rule;
    }
}
