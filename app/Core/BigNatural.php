<?php
declare(strict_types=1);

namespace Northfield\Core;

/** Pequena aritmética de inteiros decimais não negativos, sem float e sem extensões externas.
 * Apenas operações sobre números positivos de dimensão limitada pela validação da API.
 */
final class BigNatural
{
    public static function normalize(string $n): string
    {
        if (!preg_match('/^\d+$/D', $n)) {
            throw new \InvalidArgumentException('Inteiro decimal inválido.');
        }
        return ltrim($n, '0') ?: '0';
    }

    public static function compare(string $a, string $b): int
    {
        $a = self::normalize($a);
        $b = self::normalize($b);
        return strlen($a) <=> strlen($b) ?: strcmp($a, $b) <=> 0;
    }

    public static function add(string $a, string $b): string
    {
        $a = strrev(self::normalize($a)); $b = strrev(self::normalize($b));
        $len = max(strlen($a), strlen($b)); $carry = 0; $out = '';
        for ($i = 0; $i < $len; ++$i) {
            $sum = (int)($a[$i] ?? '0') + (int)($b[$i] ?? '0') + $carry;
            $out .= (string)($sum % 10); $carry = intdiv($sum, 10);
        }
        if ($carry) $out .= (string)$carry;
        return strrev($out);
    }

    public static function subtract(string $a, string $b): string
    {
        if (self::compare($a, $b) < 0) throw new \InvalidArgumentException('Subtração negativa não suportada.');
        $a = strrev(self::normalize($a)); $b = strrev(self::normalize($b));
        $borrow = 0; $out = '';
        for ($i=0; $i<strlen($a); ++$i) {
            $digit = (int)$a[$i] - (int)($b[$i] ?? '0') - $borrow;
            if ($digit < 0) { $digit += 10; $borrow = 1; } else $borrow = 0;
            $out .= (string)$digit;
        }
        return self::normalize(strrev($out));
    }

    public static function multiply(string $a, string $b): string
    {
        $a = strrev(self::normalize($a)); $b = strrev(self::normalize($b));
        if ($a === '0' || $b === '0') return '0';
        $digits = array_fill(0, strlen($a) + strlen($b), 0);
        for ($i=0; $i<strlen($a); ++$i) {
            for ($j=0; $j<strlen($b); ++$j) {
                $digits[$i+$j] += (int)$a[$i] * (int)$b[$j];
            }
        }
        for ($i=0; $i<count($digits)-1; ++$i) {
            $digits[$i+1] += intdiv($digits[$i], 10);
            $digits[$i] %= 10;
        }
        return self::normalize(implode('', array_reverse($digits)));
    }

    /** @return array{0:string,1:string} quociente e resto */
    public static function divide(string $a, string $b): array
    {
        $a = self::normalize($a); $b = self::normalize($b);
        if ($b === '0') throw new \DivisionByZeroError();
        $q = ''; $remainder = '0';
        foreach (str_split($a) as $digit) {
            $remainder = self::normalize($remainder . $digit);
            $qDigit = 0;
            while (self::compare($remainder, $b) >= 0) {
                $remainder = self::subtract($remainder, $b); ++$qDigit;
            }
            $q .= (string)$qDigit;
        }
        return [self::normalize($q), $remainder];
    }

    /** Arredondamento decimal aritmético half-up em inteiros não negativos. */
    public static function divideRounded(string $numerator, string $denominator): string
    {
        [$q, $r] = self::divide($numerator, $denominator);
        return self::compare(self::multiply($r, '2'), $denominator) >= 0 ? self::add($q, '1') : $q;
    }
}
