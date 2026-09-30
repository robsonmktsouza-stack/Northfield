<?php
declare(strict_types=1);
require __DIR__ . '/../app/Core/BigNatural.php';
require __DIR__ . '/../app/Core/Money.php';
require __DIR__ . '/../app/Fiscal/PgdasCalculator.php';

use Northfield\Core\BigNatural as B;
use Northfield\Fiscal\PgdasCalculator;
$calc = new PgdasCalculator(__DIR__ . '/../fiscal-rules/2026/anexo-i.json');
$base = ['competencia'=>'2026-09','anexo'=>'I','regime'=>'competencia','operacao_comum'=>true,'mais_de_12_meses'=>true,'sem_situacoes_especiais'=>true];
$tests = 0;
function eq(mixed $actual, mixed $expected, string $label): void {
    global $tests;
    ++$tests;
    if ($actual !== $expected) { fwrite(STDERR, "FALHOU: $label | esperado=".var_export($expected,true)." atual=".var_export($actual,true)."\n"); exit(1); }
}
function fails(callable $cb, string $label): void { global $tests; ++$tests; try{$cb();}catch(\InvalidArgumentException|\DomainException $e){return;}fwrite(STDERR,"FALHOU: exceção esperada em $label\n");exit(1); }
foreach ([['1','3','4'],['10','9','19'],['999','1','1000'],['1000','10','1010'],['25','4','29'],['999999','1','1000000'],['0','42','42']] as $v) {
    eq(B::add($v[0],$v[1]), $v[2], 'BigNatural add');
}
eq(B::multiply('360000000','1430'), '514800000000', 'multiply');
eq(B::divideRounded('56500','100'),'565', 'divide');
eq(B::divideRounded('56501','100'),'565', 'round down');
eq(B::divideRounded('56550','100'),'566', 'half up');
$r = $calc->calculate($base + ['rbt12'=>'360000.00','receita_mensal'=>'10000.00']);
eq($r['total_estimado'], '565.00', 'Anexo I RBT12 360k / receita 10k');
eq($r['aliquota_efetiva_percentual'], '5.650000', 'Aliquota efetiva 5.65');
eq($r['faixa'], 2, 'Faixa 2 borda superior');
$r = $calc->calculate($base + ['rbt12'=>'180000.00','receita_mensal'=>'10000.00']);
eq($r['faixa'], 1, 'Faixa 1 borda superior');
eq($r['total_estimado'], '400.00', '4 pct');
$r = $calc->calculate($base + ['rbt12'=>'180000.01','receita_mensal'=>'10000.00']);
eq($r['faixa'], 2, 'Faixa 2 borda inferior');
$r = $calc->calculate($base + ['rbt12'=>'450000.00','receita_mensal'=>'42500.00']);
eq($r['faixa'], 3, 'Faixa 3');
eq($r['total_estimado'], '2728.50', '450k effective 6.42 percent');
$r = $calc->calculate($base + ['rbt12'=>'3600000.00','receita_mensal'=>'0.00']);
eq($r['faixa'], 5, 'sublimite incl');
eq($r['total_estimado'], '0.00', 'sem receita');
fails(fn() => $calc->calculate($base + ['rbt12'=>'3600000.01','receita_mensal'=>'100.00']), 'acima do sublimite');
fails(fn() => $calc->calculate(array_merge($base, ['regime'=>'caixa','rbt12'=>'100000.00','receita_mensal'=>'100.00'])), 'regime caixa');
fails(fn() => $calc->calculate(array_merge($base, ['competencia'=>'2027-01','rbt12'=>'100000.00','receita_mensal'=>'100.00'])), '2027');
fails(fn() => $calc->calculate($base + ['rbt12'=>'100000.00','receita_mensal'=>'1,00']), 'decimal ptbr');
fails(fn() => $calc->calculate(array_merge($base, ['sem_situacoes_especiais'=>false,'rbt12'=>'100000.00','receita_mensal'=>'100.00'])), 'especial');
echo "OK: $tests verificações de aritmética e escopo fiscal.\n";
