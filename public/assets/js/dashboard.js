import {$,el,money,period} from './util.js';
import {listRecords} from './db.js';
export async function init(){
  const rec=await listRecords();$('metricSaved').textContent=String(rec.length);
  $('metricPeriods').textContent=String(new Set(rec.map(x=>x.input?.competencia)).size);
  const tbody=$('recentRows');tbody.replaceChildren();
  if(!rec.length){const row=el('tr');const td=el('td','Você ainda não salvou nenhuma apuração neste navegador.','table-empty');td.colSpan=3;row.append(td);tbody.append(row);return;}
  for(const r of rec.slice(0,6)){
    const tr=el('tr'), name=el('td');
    const a=el('a',String(r.empresa||'Apuração sem identificação').slice(0,100),'table-title');a.href=`/?page=apuracao&registro=${encodeURIComponent(r.id)}`;name.append(a);
    const aperiod=el('td',period(r.input?.competencia)), result=el('td',money(r.resultado?.total_estimado));tr.append(name,aperiod,result);tbody.append(tr);
  }
}
