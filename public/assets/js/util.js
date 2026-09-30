/** Helpers de apresentação: nunca usados como fonte de precisão matemática fiscal. */
export const $ = (id) => document.getElementById(id);
export const money = (v) => Number.isFinite(Number(v))
  ? new Intl.NumberFormat('pt-BR', {style:'currency',currency:'BRL'}).format(Number(v))
  : '—';
export const percent = (v, digits=2) => Number.isFinite(Number(v))
  ? new Intl.NumberFormat('pt-BR', {minimumFractionDigits:digits,maximumFractionDigits:digits}).format(Number(v)) + '%'
  : '—';
export const period = (v) => /^\d{4}-(0[1-9]|1[0-2])$/.test(v||'') ? `${v.slice(5)}/${v.slice(0,4)}` : '—';
export function parseCurrency(value) {
  if (typeof value !== 'string') throw new Error('Informe um valor monetário.');
  const s = value.trim().replace(/^R\$\s*/,'').replaceAll(' ','');
  let normalized;
  if (/^(?:0|[1-9]\d{0,2})(?:\.\d{3})*,\d{1,2}$/.test(s) || /^\d+,\d{1,2}$/.test(s)) normalized = s.replaceAll('.','').replace(',','.');
  else if (/^(?:0|[1-9]\d{0,2})(?:\.\d{3})*$/.test(s)) normalized = s.replaceAll('.','');
  else if (/^\d{1,8}(?:\.\d{1,2})?$/.test(s)) normalized = s;
  else if (/^\d{1,8}$/.test(s)) normalized = s;
  else throw new Error(`Valor inválido: ${value}`);
  const parts = normalized.split('.');
  if (parts[0].length > 8) throw new Error('O valor informado ultrapassa o limite permitido.');
  return `${String(BigInt(parts[0]))}.${(parts[1]||'').padEnd(2,'0')}`;
}
export function el(tag, text, className) {
  const node = document.createElement(tag);
  if (text !== undefined && text !== null) node.textContent = text;
  if (className) node.className = className;
  return node;
}
export function toast(message, variant='normal') {
  const box = $('toastBox'); if (!box) return;
  const item = el('div', String(message).slice(0,300), `toast ${variant}`);
  box.append(item);
  setTimeout(()=>item.remove(), 5500);
}
export function confirmAction(message) {
  return new Promise((resolve) => {
    const dialog = $('confirmDialog');
    if (!dialog || typeof dialog.showModal !== 'function') { resolve(window.confirm(message)); return; }
    $('confirmText').textContent = message;
    const okay = $('confirmYes'), cancel = $('confirmNo');
    let answered = false;
    const finish = (v) => { if(answered) return; answered=true; cleanup(); dialog.close(); resolve(v); };
    const onYes=()=>finish(true), onNo=()=>finish(false), onClose=()=>{ if(!answered){ answered=true;cleanup();resolve(false); } };
    const cleanup=()=>{okay.removeEventListener('click',onYes);cancel.removeEventListener('click',onNo);dialog.removeEventListener('close',onClose);};
    okay.addEventListener('click',onYes);cancel.addEventListener('click',onNo);dialog.addEventListener('close',onClose);
    dialog.showModal();
  });
}
