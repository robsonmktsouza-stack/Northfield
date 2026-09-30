import {$, toast} from './util.js';
import {listRecords, putRecord} from './db.js';
const ENCODER = new TextEncoder(), DECODER = new TextDecoder();
function bytesToBase64(arr){let binary='';for(let i=0;i<arr.length;i+=8192)binary+=String.fromCharCode(...arr.slice(i,i+8192));return btoa(binary);}
function base64ToBytes(text){return Uint8Array.from(atob(text),ch=>ch.charCodeAt(0));}
async function keyFor(pass,salt){const source=await crypto.subtle.importKey('raw',ENCODER.encode(pass),'PBKDF2',false,['deriveKey']);return crypto.subtle.deriveKey({name:'PBKDF2',salt,iterations:250000,hash:'SHA-256'},source,{name:'AES-GCM',length:256},false,['encrypt','decrypt']);}
export async function encryptBackup(data,password){
  if (!window.isSecureContext || !crypto.subtle) throw new Error('Backup criptografado exige HTTPS ou localhost.');
  if (password.length<10) throw new Error('A senha precisa ter pelo menos 10 caracteres.');
  const salt=crypto.getRandomValues(new Uint8Array(16));const iv=crypto.getRandomValues(new Uint8Array(12));
  const key=await keyFor(password,salt);
  const payload=ENCODER.encode(JSON.stringify({tipo:'northfield-backup',versao:1,exportadoEm:new Date().toISOString(),records:data}));
  const ciphertext=new Uint8Array(await crypto.subtle.encrypt({name:'AES-GCM',iv},key,payload));
  return JSON.stringify({formato:'MF-BACKUP-AESGCM',versao:1,kdf:'PBKDF2-SHA256-250000',salt:bytesToBase64(salt),iv:bytesToBase64(iv),dados:bytesToBase64(ciphertext)},null,2);
}
export async function decryptBackup(text,password){
  if (!window.isSecureContext || !crypto.subtle) throw new Error('Importação criptografada exige HTTPS ou localhost.');
  const doc=JSON.parse(text);
  if(doc?.formato!=='MF-BACKUP-AESGCM'||doc.versao!==1||doc.kdf!=='PBKDF2-SHA256-250000')throw new Error('Formato de backup inválido ou não suportado.');
  const salt=base64ToBytes(doc.salt),iv=base64ToBytes(doc.iv),ciphertext=base64ToBytes(doc.dados);
  if(salt.length!==16||iv.length!==12||ciphertext.length>4*1024*1024)throw new Error('Backup criptografado inválido ou muito grande.');
  const key=await keyFor(password,salt);
  let decrypted;
  try{decrypted=await crypto.subtle.decrypt({name:'AES-GCM',iv},key,ciphertext);}catch{throw new Error('Senha incorreta ou arquivo modificado.');}
  const payload=JSON.parse(DECODER.decode(decrypted));
  if(payload.tipo!=='northfield-backup'||payload.versao!==1||!Array.isArray(payload.records)||payload.records.length>1000)throw new Error('Conteúdo do backup inválido.');
  for(const r of payload.records){
    if(!r||typeof r!=='object'||typeof r.empresa!=='string'||r.empresa.length>100||typeof r.input?.competencia!=='string'||!/^2026-(0[1-9]|1[0-2])$/.test(r.input.competencia)||typeof r.resultado?.total_estimado!=='string'||!/^\d{1,8}\.\d{2}$/.test(r.resultado.total_estimado))throw new Error('O backup contém registro inválido.');
  }
  return payload.records;
}
function getPassword(title,desc,importing=false){
  return new Promise(resolve=>{
    const dlg=$('passwordDialog'),form=$('passwordForm'),input=$('passwordInput');
    if(!dlg?.showModal){resolve(null);return;}
    $('passwordTitle').textContent=title;$('passwordHelp').textContent=desc;
    $('passwordConfirm').textContent=importing?'Descriptografar':'Proteger e baixar';
    input.value='';input.minLength=importing?1:10;
    let handled=false;
    function finish(value){if(handled)return;handled=true;form.removeEventListener('submit',submit);$('passwordClose').removeEventListener('click',close);$('passwordCancel').removeEventListener('click',close);dlg.removeEventListener('close',close);dlg.close();resolve(value);}
    function submit(e){e.preventDefault();if(!form.reportValidity())return;finish(input.value);}
    function close(){finish(null);}
    form.addEventListener('submit',submit);$('passwordClose').addEventListener('click',close);$('passwordCancel').addEventListener('click',close);dlg.addEventListener('close',close);
    dlg.showModal();input.focus();
  });
}
function download(text,filename){const url=URL.createObjectURL(new Blob([text],{type:'application/json'}));const a=document.createElement('a');a.href=url;a.download=filename;a.click();setTimeout(()=>URL.revokeObjectURL(url),1000);}
export async function exportLocalBackup(){
  try{const records=await listRecords();if(!records.length){toast('Não há apurações para exportar.','warning');return;}
  const password=await getPassword('Exportar backup criptografado',`Serão protegidos ${records.length} registros usando criptografia AES-GCM. Escolha uma senha de pelo menos 10 caracteres.`);if(password===null)return;
  const file=await encryptBackup(records,password);download(file,`northfield-backup-${new Date().toISOString().slice(0,10)}.mfbackup`);toast('Backup protegido gerado. Guarde a senha com segurança.');
  }catch(e){toast(e.message,'error');}
}
export async function importLocalBackup(file){
  if(!file)return 0;
  if(file.size>6*1024*1024){toast('O arquivo excede 6 MB.','error');return 0;}
  const password=await getPassword('Importar backup criptografado','Informe a senha original do arquivo.',true);if(password===null)return 0;
  try{const records=await decryptBackup(await file.text(),password);
    for(const r of records){const imported={id:crypto.randomUUID(),empresa:r.empresa,input:{...r.input},resultado:{...r.resultado},updatedAt:new Date().toISOString(),importado:true};await putRecord(imported);}
    toast(`${records.length} apuração(ões) importada(s) neste navegador.`);return records.length;
  }catch(e){toast(e.message,'error');return 0;}
}
