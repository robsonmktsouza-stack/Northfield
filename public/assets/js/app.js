import {$,toast,confirmAction} from './util.js';
import {exportLocalBackup,importLocalBackup} from './crypto.js';
import {listRecords,clearRecords} from './db.js';

const nav=$('sidebar'),overlay=$('mobileOverlay');
$('toggleNav')?.addEventListener('click',()=>{
  const open=nav.classList.toggle('open');overlay.hidden=!open;
});
overlay?.addEventListener('click',()=>{nav.classList.remove('open');overlay.hidden=true;});
$('globalSearch')?.addEventListener('input',(e)=>{
  const needle=e.target.value.normalize('NFD').replace(/[\u0300-\u036f]/g,'').toLowerCase().trim();
  for(const a of document.querySelectorAll('.menu-link')){
    a.hidden=needle!==''&&!a.textContent.normalize('NFD').replace(/[\u0300-\u036f]/g,'').toLowerCase().includes(needle);
  }
  if(needle && innerWidth<=830){nav.classList.add('open');overlay.hidden=false;}
});
document.addEventListener('keydown',(e)=>{
  if((e.ctrlKey||e.metaKey)&&e.key.toLowerCase()==='k'){e.preventDefault();$('globalSearch')?.focus();}
  if(e.key==='Escape'&&nav.classList.contains('open')){nav.classList.remove('open');overlay.hidden=true;}
});
const page=document.body.dataset.page;
if(page==='painel')import('./dashboard.js').then(m=>m.init()).catch(e=>toast(e.message,'error'));
if(page==='apuracao')import('./apuracao.js').then(m=>m.init()).catch(e=>toast(e.message,'error'));
if(page==='historico')import('./historico.js').then(m=>m.init()).catch(e=>toast(e.message,'error'));
if(page==='conferencia')import('./xml.js').then(m=>m.init()).catch(e=>toast(e.message,'error'));
if(page==='privacidade'){
  const refresh=async()=>{$('privacyCount').textContent=String((await listRecords()).length);};
  refresh().catch(e=>toast(e.message,'error'));
  $('privacyExport')?.addEventListener('click',exportLocalBackup);
  $('privacyClear')?.addEventListener('click',async()=>{
    if(!await confirmAction('Apagar permanentemente todos os registros fiscais salvos neste navegador? A operação não pode ser desfeita sem um backup.'))return;
    try{await clearRecords();await refresh();toast('Histórico local apagado.');}catch(e){toast(e.message,'error');}
  });
}
