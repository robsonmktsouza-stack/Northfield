import {$,el,money,percent,period,parseCurrency,toast} from './util.js';
import {putRecord,getRecord} from './db.js';
let current=null, savedId=null, restored=false;
function clearResult(){current=null;$('resultContent').hidden=true;$('resultEmpty').hidden=false;$('memoryPanel').hidden=true;}
function setError(message){const block=$('formError');block.textContent=message;block.hidden=false;block.scrollIntoView({behavior:'smooth',block:'nearest'});}
function clearError(){$('formError').textContent='';$('formError').hidden=true;}
function monthsFor(competencia){const match=/^(\d{4})-(\d{2})$/.exec(competencia);if(!match)return[];const year=Number(match[1]),month=Number(match[2]);if(month<1||month>12)return[];return Array.from({length:12},(_,i)=>{const date=new Date(Date.UTC(year,month-1-12+i,1));return{key:date.toISOString().slice(0,7),label:date.toLocaleDateString('pt-BR',{timeZone:'UTC',month:'short',year:'numeric'})};});}
function buildMonths(){const grid=$('monthsGrid');grid.replaceChildren();for(const m of monthsFor($('competencia').value)){
    const label=el('label',m.label);const input=el('input');input.type='text';input.dataset.month=m.key;input.placeholder='0,00';input.inputMode='decimal';label.append(input);grid.append(label);
    input.addEventListener('input',()=>{$('monthsSum').textContent='Preencha todos os meses para calcular a soma.';});
  }
  $('monthsSum').textContent='Soma informada: —';
}
function inputPayload(){
  return {
    competencia:$('competencia').value,anexo:$('anexo').value,regime:$('regime').value,
    receita_mensal:parseCurrency($('receitaMensal').value),rbt12:parseCurrency($('rbt12').value),
    operacao_comum:$('checkNormal').checked,mais_de_12_meses:$('checkEstablished').checked,sem_situacoes_especiais:$('checkSpecial').checked
  };
}
function addMemoryItem(grid,label,value){const wrap=el('div',null,'memory-item');wrap.append(el('span',label),el('strong',value));grid.append(wrap);}
function renderResult(result){
  $('resultEmpty').hidden=true;$('resultContent').hidden=false;$('memoryPanel').hidden=false;
  $('resultTotal').textContent=money(result.total_estimado);
  $('resultEfetiva').textContent=percent(result.aliquota_efetiva_percentual,4);
  $('resultFaixa').textContent=result.faixa+'ª faixa';
  $('resultNominal').textContent=percent(result.aliquota_nominal_percentual);
  $('resultDeduzir').textContent=money(result.parcela_deduzir);
  $('resultRbt').textContent=money(result.rbt12);
  $('resultRevenue').textContent=money(result.receita_mensal);
  const taxBody=$('resultTaxes');taxBody.replaceChildren();
  for(const tax of result.reparticao_indicativa||[]){const row=el('div',null,'tax-row');const label=el('span',`${tax.tributo} (${percent(tax.participacao_percentual)})`);row.append(label,el('b',money(tax.valor)));taxBody.append(row);}
  const mem=$('memoryContent');mem.replaceChildren();
  if(restored){const old=el('p','Registro local carregado. O resultado arquivado não foi recalculado com as regras atuais. Clique em Calcular apuração antes de utilizar o relatório.','note-small');mem.append(old);}
  const grid=el('div',null,'memory-grid');
  addMemoryItem(grid,'Empresa (identificação local)',($('empresa').value.trim()||'Não informada').slice(0,100));
  addMemoryItem(grid,'Competência',period(result.competencia));
  addMemoryItem(grid,'Fonte da regra',result.regra?.id||'—');
  addMemoryItem(grid,'Versão do catálogo',result.regra?.versao||'—');
  addMemoryItem(grid,'Receita mensal',money(result.receita_mensal));
  addMemoryItem(grid,'RBT12',money(result.rbt12));
  addMemoryItem(grid,'Fórmula',result.memoria?.formula||'—');
  addMemoryItem(grid,'Repartição',result.memoria?.reparticao||'—');
  mem.append(grid);
  const note=el('p','RESTRIÇÕES: '+(result.limitacoes||[]).join(' '),'note-small');note.style.marginTop='17px';mem.append(note);
  const refs=el('p','Fontes oficiais: ');
  for(const item of result.regra?.referencias||[]){const a=el('a',item.nome);try { const url = new URL(item.url); if (url.protocol !== 'https:' || !['www.planalto.gov.br','planalto.gov.br','normas.receita.fazenda.gov.br','www8.receita.fazenda.gov.br'].includes(url.hostname)) continue; a.href=url.href; } catch { continue; } a.target='_blank';a.rel='noopener noreferrer';a.style.color='#236eaf';refs.append(a,document.createTextNode(' • '));}
  mem.append(refs);
  const version=el('p',`Diferença de arredondamento indicativo (total menos somatório dos tributos): ${money(result.diferenca_arredondamento)}. Não se presume conformidade com o PGDAS-D oficial.`);
  mem.append(version);
}
async function calculate(ev){ev.preventDefault();clearError();
  let payload;
  try{payload=inputPayload();if(!payload.operacao_comum||!payload.mais_de_12_meses||!payload.sem_situacoes_especiais)throw new Error('Marque as três confirmações para utilizar o escopo simplificado.');}
  catch(e){setError(e.message);return;}
  const button=$('calcButton');button.disabled=true;button.textContent='Calculando...';
  try{
    const res=await fetch('/api/calcular',{method:'POST',headers:{'Content-Type':'application/json','Accept':'application/json'},body:JSON.stringify(payload),cache:'no-store',credentials:'same-origin'});
    const body=await res.json();if(!res.ok||body.ok!==true)throw new Error(body.erro||'Erro no processamento.');
    current={input:payload,resultado:body.resultado};restored=false;renderResult(body.resultado);toast('Simulação matemática concluída. Confira as limitações.');
  }catch(e){clearResult();setError(e.message||'Erro inesperado na comunicação.');}
  finally{button.disabled=false;button.innerHTML='<svg class="icon" aria-hidden="true"><use href="#i-calculator"></use></svg> Calcular apuração';}
}
async function save(){if(!current){toast('Calcule antes de salvar.','warning');return;}
  const record={id:savedId||crypto.randomUUID(),empresa:$('empresa').value.trim().slice(0,100),input:{...current.input},resultado:{...current.resultado},updatedAt:new Date().toISOString(),importado:false};
  try{await putRecord(record);savedId=record.id;toast('Apuração salva apenas neste navegador.');}catch(e){toast(e.message,'error');}
}
async function restore(){const id=new URLSearchParams(location.search).get('registro');if(!id)return;
  try{const record=await getRecord(id);if(!record){toast('Este registro não foi encontrado no histórico local.','warning');return;}
    savedId=id;$('empresa').value=String(record.empresa||'').slice(0,100);
    $('competencia').value=record.input.competencia;buildMonths();
    $('receitaMensal').value=record.input.receita_mensal.replace('.',',');$('rbt12').value=record.input.rbt12.replace('.',',');
    $('checkNormal').checked=record.input.operacao_comum===true;$('checkEstablished').checked=record.input.mais_de_12_meses===true;$('checkSpecial').checked=record.input.sem_situacoes_especiais===true;
    current={input:record.input,resultado:record.resultado};restored=true;renderResult(record.resultado);
    toast('Apuração local carregada. Recalcule para verificar as regras atuais.','warning');
  }catch(e){toast(e.message,'error');}
}
export async function init(){
  buildMonths();$('competencia').addEventListener('change',buildMonths);
  $('calcForm').addEventListener('submit',calculate);
  $('calcForm').addEventListener('reset',()=>{savedId=null;setTimeout(()=>{clearResult();clearError();buildMonths();},0);});
  $('useMonths').addEventListener('click',()=>{
    try{const inputs=[...$('monthsGrid').querySelectorAll('input')];if(inputs.length!==12||inputs.some(x=>x.value.trim()===''))throw new Error('Preencha todos os 12 meses, utilizando 0,00 quando não houve receita.');
      let cents=0n;for(const input of inputs)cents+=BigInt(parseCurrency(input.value).replace('.',''));
      const decimal=`${cents/100n}.${String(cents%100n).padStart(2,'0')}`;
      $('rbt12').value=decimal.replace('.',',');$('monthsSum').textContent=`Soma informada: ${money(decimal)}`;toast('RBT12 atualizada pela soma dos 12 meses informados.');
    }catch(e){toast(e.message,'error');}
  });
  $('saveRecord').addEventListener('click',save);
  $('printRecord').addEventListener('click',()=>{if(!current)return;window.print();});
  await restore();
}
