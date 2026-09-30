<section class="notice notice-warning"><span class="notice-icon"><?= $symbol('alert') ?></span><div><strong>Cobertura restrita — leia antes de calcular</strong><p>Somente Anexo I, competência 2026, empresa com mais de 12 meses, regime de competência e receita de revenda comum, sem situações especiais. Simulação não homologada.</p></div></section>
<div class="form-workspace">
  <section class="panel calculation-panel">
    <div class="panel-header"><div><h2>Informações da apuração</h2><p>Preencha os valores usados no cálculo.</p></div><span class="pill pill-muted">ETAPA 1 DE 2</span></div>
    <form id="calcForm" novalidate>
      <div class="form-section-title">Identificação local <span>Não enviada ao servidor</span></div>
      <div class="form-grid two"><label class="field"><span>Identificação da empresa <em>opcional</em></span><input id="empresa" maxlength="100" placeholder="Ex.: Empresa exemplo LTDA" autocomplete="off"><small>Usada apenas no relatório e no histórico local.</small></label><label class="field"><span>Competência</span><input id="competencia" type="month" value="2026-09" min="2026-01" max="2026-12" required></label></div>
      <div class="form-grid two"><label class="field"><span>Atividade</span><select id="anexo" required><option value="I">Comércio — Anexo I</option><option value="II" disabled>Indústria — Anexo II (em desenvolvimento)</option><option value="III" disabled>Serviços — Anexo III (em desenvolvimento)</option><option value="IV" disabled>Serviços — Anexo IV (em desenvolvimento)</option><option value="V" disabled>Serviços — Anexo V (em desenvolvimento)</option></select></label><label class="field"><span>Regime de reconhecimento</span><select id="regime"><option value="competencia">Competência</option><option disabled>Caixa (ainda não suportado)</option></select></label></div>
      <div class="form-section-title">Valores da competência <span>Informe somente números da apuração</span></div>
      <div class="form-grid two"><label class="field"><span>Receita bruta mensal (R$) <strong class="required">*</strong></span><div class="input-prefix"><span>R$</span><input id="receitaMensal" inputmode="decimal" placeholder="0,00" value="" required></div><small>Receita comum sujeita integralmente ao Anexo I.</small></label><label class="field"><span>RBT12 (R$) <strong class="required">*</strong></span><div class="input-prefix"><span>R$</span><input id="rbt12" inputmode="decimal" placeholder="0,00" value="" required></div><small>Receita bruta dos 12 meses anteriores à competência.</small></label></div>
      <details class="history-details" id="historyDetails"><summary><?= $symbol('history') ?> Preencher RBT12 mês a mês <?= $symbol('chevron') ?></summary><div class="history-inside"><p>Informe os doze meses anteriores. Um mês sem receitas deve ser preenchido com zero; não deixe meses desconhecidos em branco.</p><div id="monthsGrid" class="months-grid"></div><div class="months-actions"><span id="monthsSum">Soma informada: —</span><button type="button" id="useMonths" class="btn btn-outline btn-mini">Usar soma na RBT12</button></div></div></details>
      <div class="form-section-title">Condições para esta versão <span>Confirmações obrigatórias</span></div>
      <div class="check-list"><label><input type="checkbox" id="checkNormal" required><span>A receita corresponde a <b>revenda comum</b>, integralmente sujeita ao Anexo I.</span></label><label><input type="checkbox" id="checkEstablished" required><span>Empresa possui <b>mais de 12 meses de atividade</b> e a RBT12 está correta.</span></label><label><input type="checkbox" id="checkSpecial" required><span>Não há ST, monofásico, exportações, devoluções, isenções, regime especial, excesso de sublimite ou outra condição que exija segregação.</span></label></div>
      <div id="formError" class="form-error" hidden role="alert"></div>
      <div class="form-actions"><button type="reset" class="btn btn-outline" id="clearForm">Limpar campos</button><button class="btn btn-primary" type="submit" id="calcButton"><?= $symbol('calculator') ?> Calcular apuração</button></div>
    </form>
  </section>
  <aside class="right-column">
    <section class="panel result-panel" id="resultPanel"><div class="panel-header"><div><h2>Resultado da apuração</h2><p>Memória de cálculo com referência normativa</p></div></div>
      <div id="resultEmpty" class="result-empty"><?= $symbol('calculator') ?><strong>Aguardando cálculo</strong><p>Preencha os dados e clique em “Calcular apuração” para visualizar os valores.</p></div>
      <div id="resultContent" hidden>
        <div class="result-total"><span>Valor matemático estimado</span><strong id="resultTotal">—</strong><small>Não equivale a DAS emitido</small></div>
        <div class="result-kpis"><div><span>Alíquota efetiva</span><strong id="resultEfetiva">—</strong></div><div><span>Faixa de receita</span><strong id="resultFaixa">—</strong></div></div>
        <div class="result-mini-row"><span>Alíquota nominal</span><b id="resultNominal">—</b></div><div class="result-mini-row"><span>Parcela a deduzir</span><b id="resultDeduzir">—</b></div><div class="result-mini-row"><span>RBT12</span><b id="resultRbt">—</b></div><div class="result-mini-row"><span>Receita do mês</span><b id="resultRevenue">—</b></div>
        <div class="result-subtitle">Repartição indicativa</div><div id="resultTaxes"></div>
        <div class="note-small">Os valores de cada tributo podem divergir em centavos do sistema oficial. Não utilize esta simulação como declaração.</div>
        <div class="result-controls"><button type="button" id="saveRecord" class="btn btn-primary"><?= $symbol('save') ?> Salvar neste navegador</button><button type="button" id="printRecord" class="btn btn-outline"><?= $symbol('print') ?> Imprimir relatório</button></div>
      </div>
    </section>
    <section class="panel instruction-panel"><h3><?= $symbol('shield') ?> Proteção de dados</h3><p>O PHP recebe apenas competência, receita, RBT12 e confirmações do escopo. O nome da empresa não é enviado para cálculo.</p><a href="/?page=privacidade">Como funciona a privacidade <?= $symbol('arrow') ?></a></section>
  </aside>
</div>
<section class="panel memory-panel" id="memoryPanel" hidden><div class="panel-header"><div><h2>Memória de cálculo</h2><p>Fundamentação e informações do processamento</p></div><span class="pill pill-warning">NÃO HOMOLOGADO</span></div><div id="memoryContent" class="memory-content"></div></section>
