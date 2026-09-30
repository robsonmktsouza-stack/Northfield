<?php
declare(strict_types=1);

namespace Northfield\Core;

final class Money
{
    /** Aceita apenas decimal canônico 1000.00, sem separador de milhares. */
    public static function toCents(mixed $amount): string
    {
        if (!is_string($amount) || !preg_match('/^(0|[1-9]\d{0,7})(?:\.(\d{1,2}))?$/D', $amount, $m)) {
            throw new \InvalidArgumentException('Informe um valor monetário válido, até R$ 99.999.999,99.');
        }
        return BigNatural::normalize($m[1] . str_pad($m[2] ?? '', 2, '0'));
    }

    public static function fromCents(string $cents): string
    {
        $cents = BigNatural::normalize($cents);
        $cents = str_pad($cents, 3, '0', STR_PAD_LEFT);
        return substr($cents, 0, -2) . '.' . substr($cents, -2);
    }
}
