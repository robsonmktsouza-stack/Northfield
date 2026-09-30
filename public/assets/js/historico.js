import {$,el,money,period,toast,confirmAction} from './util.js';
import {listRecords,deleteRecord} from './db.js';
import {exportLocalBackup,importLocalBackup} from './crypto.js';
let records=[];
function render(){const filter=$('historySearch').value.normalize('NFD').replace(/[\u0300-\u036f]/g,'').toLowerCase();const filtered=records.filter(r=>`${r.empresa||''} ${r.input?.competencia||''}`.normalize('NFD').replace(/[\u0300-\u036f]/g,'').toLowerCase().includes(filter));
  const body=$('historyRows');body.replaceChildren();$('historyCount').textContent=`${filtered.length} registro(s)`;
  if(!filtered.length){const tr=el('tr'),td=el('td',filter?'Nenhuma apuração corresponde à busca.':'Você ainda não possui apurações salvas.','table-empty');td.colSpan=6;tr.append(td);body.append(tr);return;}
  for(const record of filtered){const tr=el('tr'),tdName=el('td');
    const title=el('a',record.empresa||'Apuração sem identificação','table-title');title.href=`/?page=apuracao&registro=${encodeURIComponent(record.id)}`;
    tdName.append(title,el('div',record.importado?'Importado de backup • simulação':'Simulação não homologada','table-subtitle'));
    const tdActions=el('td');tdActions.className='td-actions';const wrap=el('div',null,'row-actions');
    const open=el('a',null,'row-action');open.href=`/?page=apuracao&registro=${encodeURIComponent(record.id)}`;open.title='Abrir apuração';open.textContent='↗';
    const remove=el('button','×','row-action danger');remove.title='Excluir apuração';remove.type='button';
    remove.addEventListener('click',async()=>{if(!await confirmAction('Excluir esta apuração do histórico local?'))return;try{await deleteRecord(record.id);await refresh();toast('Registro excluído.');}catch(e){toast(e.message,'error');}});
    wrap.append(open,remove);tdActions.append(wrap);
    tr.append(tdName,el('td',period(record.input?.competencia)),el('td',money(record.input?.receita_mensal)),el('td',money(record.resultado?.total_estimado)),el('td',new Date(record.updatedAt).toLocaleString('pt-BR')),tdActions);body.append(tr);
  }
}
async function refresh(){records=await listRecords();render();}
export async function init(){await refresh();$('historySearch').addEventListener('input',render);$('reloadHistory').addEventListener('click',()=>refresh().catch(e=>toast(e.message,'error')));$('exportBackup').addEventListener('click',exportLocalBackup);
  $('importBackup').addEventListener('click',()=>$('backupFileInput').click());
  $('backupFileInput').addEventListener('change',async e=>{await importLocalBackup(e.target.files[0]);e.target.value='';await refresh();});
}
