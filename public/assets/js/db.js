const NAME='northfield-local-v1', STORE='apuracoes';
export async function openDB() {
  if (!('indexedDB' in window)) throw new Error('IndexedDB indisponível neste navegador.');
  return await new Promise((resolve,reject)=>{
    const req=indexedDB.open(NAME,1);
    req.onupgradeneeded=()=>{const db=req.result;if(!db.objectStoreNames.contains(STORE)) db.createObjectStore(STORE,{keyPath:'id'});};
    req.onsuccess=()=>resolve(req.result);
    req.onerror=()=>reject(new Error('Não foi possível acessar o histórico local.'));
    req.onblocked=()=>reject(new Error('Feche outras abas do Northfield para continuar.'));
  });
}
async function transaction(mode, callback) {
  const db=await openDB();
  try {return await new Promise((resolve,reject)=>{
    const tx=db.transaction(STORE,mode), store=tx.objectStore(STORE);
    let req, value;
    try {req=callback(store); }catch(e){reject(e);return;}
    req.onsuccess=()=>{value=req.result;};
    req.onerror=()=>reject(new Error('Falha na operação IndexedDB.'));
    tx.oncomplete=()=>resolve(value);
    tx.onerror=()=>reject(new Error('Erro ao persistir os dados locais.'));
    tx.onabort=()=>reject(new Error('Operação local abortada.'));
  });} finally {db.close();}
}
export async function listRecords(){const records=await transaction('readonly',s=>s.getAll());return records.sort((a,b)=>String(b.updatedAt).localeCompare(String(a.updatedAt)));}
export async function getRecord(id){return transaction('readonly',s=>s.get(id));}
export async function putRecord(record){return transaction('readwrite',s=>s.put(record));}
export async function deleteRecord(id){return transaction('readwrite',s=>s.delete(id));}
export async function clearRecords(){return transaction('readwrite',s=>s.clear());}
