import {$,el,money,toast} from './util.js';
let documents=[];
function descendants(node,name){return node?.getElementsByTagNameNS('*',name)||[];}
function firstText(node,name){const list=descendants(node,name);return list.length?list[0].textContent?.trim()||'':'';}
function parseNfe(file,text){
  if(/<!\s*(DOCTYPE|ENTITY)/i.test(text))throw new Error('XML com DTD ou entidades não é aceito.');
  const doc=new DOMParser().parseFromString(text,'application/xml');
  if(doc.querySelector('parsererror'))throw new Error('Estrutura XML inválida.');
  const infNfe=descendants(doc,'infNFe')[0];if(!infNfe)throw new Error('Este XML não contém uma NF-e reconhecível.');
  const key=(infNfe.getAttribute('Id')||'').replace(/^NFe/,'');
  if(!/^\d{44}$/.test(key))throw new Error('Chave de acesso NF-e ausente ou inválida.');
  const totals=descendants(infNfe,'ICMSTot')[0];
  const vNF=firstText(totals,'vNF');
  if(!/^\d{1,10}\.\d{2}$/.test(vNF))throw new Error('Valor vNF ausente ou inválido.');
  const date=firstText(infNfe,'dhEmi')||firstText(infNfe,'dEmi');
  const prot=descendants(doc,'infProt')[0], status=firstText(prot,'cStat');
  const authorized=['100','150'].includes(status);
  return {filename:file.name.slice(0,100),key,keyShort:`…${key.slice(-11)}`,issued:date?date.slice(0,10).split('-').reverse().join('/'):'—',amount:vNF,status:authorized?'Com protocolo autorizado':(prot?`Protocolo cStat ${status||'—'}`:'Sem protocolo no XML'),authorized};
}
function refresh(){const table=$('xmlRows');table.replaceChildren();$('xmlAnalyzed').textContent=String(documents.length);$('xmlAuthorized').textContent=String(documents.filter(x=>x.authorized).length);const sum=documents.reduce((acc,r)=>acc+BigInt(r.amount.replace('.','')),0n);$('xmlTotal').textContent=money(`${sum/100n}.${String(sum%100n).padStart(2,'0')}`);
  $('xmlResult').hidden=!documents.length;
  for(const doc of documents){const tr=el('tr'),statusTd=el('td');statusTd.append(el('span',doc.status,doc.authorized?'pill pill-success':'pill pill-warning'));
    tr.append(el('td',doc.filename),el('td',doc.issued),el('td',doc.keyShort),el('td',money(doc.amount)),statusTd);table.append(tr);
  }
}
async function readFiles(fileList){const files=[...fileList];if(files.length>30){toast('Selecione no máximo 30 arquivos por lote.','error');return;}
  let successes=0;const errors=[];const currentKeys=new Set(documents.map(x=>x.key));
  for(const file of files){if(!/\.xml$/i.test(file.name)||file.size>2*1024*1024){errors.push(`${file.name}: extensão inválida ou acima de 2 MB.`);continue;}
    try{const data=parseNfe(file,await file.text());if(currentKeys.has(data.key)){errors.push(`${file.name}: chave duplicada.`);continue;}documents.push(data);currentKeys.add(data.key);successes++;}
    catch(e){errors.push(`${file.name}: ${e.message}`);}
  }
  refresh();if(successes)toast(`${successes} arquivo(s) analisado(s) localmente.`);
  if(errors.length)toast(`${errors.length} rejeitado(s). ${errors.slice(0,2).join(' ')}`,'warning');
}
export function init(){const drop=$('xmlDropZone'),input=$('xmlInput');
  $('selectXml').addEventListener('click',()=>input.click());
  input.addEventListener('change',async()=>{await readFiles(input.files);input.value='';});
  for(const event of ['dragenter','dragover'])drop.addEventListener(event,e=>{e.preventDefault();drop.classList.add('dragging');});
  for(const event of ['dragleave','drop'])drop.addEventListener(event,e=>{e.preventDefault();drop.classList.remove('dragging');});
  drop.addEventListener('drop',e=>readFiles(e.dataTransfer.files));
  $('clearXml').addEventListener('click',()=>{documents=[];refresh();toast('Listagem temporária apagada do navegador.');});
}
