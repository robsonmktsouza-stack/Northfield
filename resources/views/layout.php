<?php
$pages = [
    'painel'=>['Painel','Visão geral das ferramentas e apurações locais.','Painel'],
    'apuracao'=>['Nova apuração','Simulação matemática do PGDAS-D • Comércio comum, Anexo I.','PGDAS-D'],
    'historico'=>['Histórico local','Apurações que você escolheu salvar neste navegador.','PGDAS-D'],
    'conferencia'=>['Conferência de XML','Leitura local de documentos NF-e, sem envio ao servidor.','PGDAS-D'],
    'regras'=>['Base normativa','Tabelas e fundamentos legais aplicados pelo motor.','Legislação'],
    'privacidade'=>['Privacidade e dados','Controle local do histórico e transparência sobre processamento.','Sistema'],
    'sobre'=>['Sobre o projeto','Código aberto, escopo e limitações desta versão.','Sistema'],
    'notfound'=>['Página não encontrada','A página solicitada não está disponível.','Sistema']
];
[$pageTitle,$pageDescription,$bread] = $pages[$page];
$esc = static fn(mixed $s): string => htmlspecialchars((string)$s, ENT_QUOTES | ENT_SUBSTITUTE, 'UTF-8');
$nav = static fn(string $destination): string => '/?page=' . rawurlencode($destination);
$upper = static fn(string $s): string => strtr(strtoupper($s), ['á'=>'Á','à'=>'À','â'=>'Â','ã'=>'Ã','é'=>'É','ê'=>'Ê','í'=>'Í','ó'=>'Ó','ô'=>'Ô','õ'=>'Õ','ú'=>'Ú','ç'=>'Ç']);
$symbol = static fn(string $id, string $class = ''): string => '<svg class="icon '.$class.'" aria-hidden="true"><use href="#i-'.$id.'"></use></svg>';
?>
<!doctype html>
<html lang="pt-BR">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width,initial-scale=1,viewport-fit=cover">
<meta name="color-scheme" content="light">
<meta name="description" content="Northfield — ferramenta fiscal gratuita e aberta para conferência de apurações do Simples Nacional.">
<title><?= $esc($pageTitle) ?> · Northfield</title>
<link rel="icon" type="image/svg+xml" href="/assets/favicon.svg">
<link rel="stylesheet" href="/assets/css/app.css?v=1.0.0">
<script type="module" src="/assets/js/app.js?v=1.0.0"></script>
</head>
<body data-page="<?= $esc($page) ?>">
<svg class="svg-sprite" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
<symbol id="i-menu" viewBox="0 0 24 24"><path d="M4 6h16M4 12h16M4 18h16"/></symbol>
<symbol id="i-grid" viewBox="0 0 24 24"><rect x="3" y="3" width="7" height="7" rx="1"/><rect x="14" y="3" width="7" height="7" rx="1"/><rect x="3" y="14" width="7" height="7" rx="1"/><rect x="14" y="14" width="7" height="7" rx="1"/></symbol>
<symbol id="i-calculator" viewBox="0 0 24 24"><rect x="5" y="2" width="14" height="20" rx="2"/><path d="M8 7h8M8 12h2M14 12h2M8 16h2M14 16h2"/></symbol>
<symbol id="i-file" viewBox="0 0 24 24"><path d="M13 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V9Z"/><path d="M13 2v7h7M8 13h8M8 17h6"/></symbol>
<symbol id="i-checklist" viewBox="0 0 24 24"><rect x="4" y="3" width="16" height="18" rx="2"/><path d="m8 9 2 2 4-4M8 16h8"/></symbol>
<symbol id="i-history" viewBox="0 0 24 24"><path d="M3 12a9 9 0 1 0 3-7M3 4v5h5M12 7v5l4 2"/></symbol>
<symbol id="i-upload" viewBox="0 0 24 24"><path d="M12 16V3M7 8l5-5 5 5M4 16v4h16v-4"/></symbol>
<symbol id="i-book" viewBox="0 0 24 24"><path d="M12 6a9 9 0 0 0-9-2v15a9 9 0 0 1 9 2M12 6a9 9 0 0 1 9-2v15a9 9 0 0 0-9 2M12 6v15"/></symbol>
<symbol id="i-shield" viewBox="0 0 24 24"><path d="m12 2 9 4v6c0 5-3.4 8.5-9 10-5.6-1.5-9-5-9-10V6l9-4Z"/><path d="m8 12 2.5 2.5 5-5"/></symbol>
<symbol id="i-lock" viewBox="0 0 24 24"><rect x="5" y="10" width="14" height="11" rx="2"/><path d="M8 10V7a4 4 0 0 1 8 0v3M12 14v3"/></symbol>
<symbol id="i-info" viewBox="0 0 24 24"><circle cx="12" cy="12" r="9"/><path d="M12 11v6M12 7h.01"/></symbol>
<symbol id="i-search" viewBox="0 0 24 24"><circle cx="11" cy="11" r="7"/><path d="m16 16 5 5"/></symbol>
<symbol id="i-bell" viewBox="0 0 24 24"><path d="M18 8a6 6 0 0 0-12 0c0 8-3 8-3 10h18c0-2-3-2-3-10M10 21h4"/></symbol>
<symbol id="i-chevron" viewBox="0 0 24 24"><path d="m6 9 6 6 6-6"/></symbol>
<symbol id="i-arrow" viewBox="0 0 24 24"><path d="M5 12h14m-6-6 6 6-6 6"/></symbol>
<symbol id="i-plus" viewBox="0 0 24 24"><path d="M12 4v16M4 12h16"/></symbol>
<symbol id="i-x" viewBox="0 0 24 24"><path d="M5 5l14 14M19 5 5 19"/></symbol>
<symbol id="i-download" viewBox="0 0 24 24"><path d="M12 3v13m-5-5 5 5 5-5M4 19v2h16v-2"/></symbol>
<symbol id="i-refresh" viewBox="0 0 24 24"><path d="M20 11a8 8 0 0 0-14-5L3 9M3 4v5h5M4 13a8 8 0 0 0 14 5l3-3M21 20v-5h-5"/></symbol>
<symbol id="i-save" viewBox="0 0 24 24"><path d="M5 3h12l4 4v14H3V3h2zM7 3v7h10V3M7 21v-8h10v8"/></symbol>
<symbol id="i-trash" viewBox="0 0 24 24"><path d="M3 6h18M9 6V4h6v2m4 0-1 15H6L5 6M10 10v7M14 10v7"/></symbol>
<symbol id="i-pencil" viewBox="0 0 24 24"><path d="m3 17 0 4 4 0L20 8l-4-4L3 17Zm10-10 4 4"/></symbol>
<symbol id="i-print" viewBox="0 0 24 24"><path d="M6 9V3h12v6M6 18H4V9h16v9h-2M6 15h12v6H6v-6Z"/></symbol>
<symbol id="i-alert" viewBox="0 0 24 24"><path d="m12 3 10 18H2L12 3ZM12 9v5m0 4h.01"/></symbol>
<symbol id="i-check" viewBox="0 0 24 24"><path d="m4 12 5 5L20 6"/></symbol>
<symbol id="i-external" viewBox="0 0 24 24"><path d="M14 4h6v6m0-6-9 9M20 14v6H4V4h6"/></symbol>
<symbol id="i-briefcase" viewBox="0 0 24 24"><rect x="2" y="7" width="20" height="14" rx="2"/><path d="M8 7V5a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2M2 13h20"/></symbol>
<symbol id="i-settings" viewBox="0 0 24 24"><path d="M4 7h16M4 17h16M9 3v8M15 13v8"/></symbol>
<symbol id="i-help" viewBox="0 0 24 24"><circle cx="12" cy="12" r="10"/><path d="M9.5 9a2.5 2.5 0 1 1 4.4 1.6C13 11.7 12 12 12 14M12 18h.01"/></symbol>
<symbol id="i-folder" viewBox="0 0 24 24"><path d="M3 6h7l2 2h9v12H3V6Z"/></symbol>
<symbol id="i-database" viewBox="0 0 24 24"><ellipse cx="12" cy="5" rx="9" ry="3"/><path d="M3 5v14c0 4 18 4 18 0V5M3 12c0 4 18 4 18 0"/></symbol>
<symbol id="i-chart" viewBox="0 0 24 24"><path d="M4 20V4M4 20h17M8 16v-5M13 16V7M18 16V4"/></symbol>
<symbol id="i-more" viewBox="0 0 24 24"><circle cx="5" cy="12" r="1"/><circle cx="12" cy="12" r="1"/><circle cx="19" cy="12" r="1"/></symbol>
</svg>
<div class="app-shell">
    <header class="topbar">
        <div class="brand-region">
            <button id="toggleNav" class="icon-btn nav-toggle" aria-label="Abrir ou fechar menu"><?= $symbol('menu') ?></button>
            <div class="brand-emblem"><?= $symbol('shield') ?></div>
            <span class="brand-word">NORTHFIELD</span>
        </div>
        <div class="header-search-wrap">
            <?= $symbol('search') ?>
            <input id="globalSearch" type="search" autocomplete="off" placeholder="Pesquisar ferramentas..." aria-label="Pesquisar ferramentas">
            <kbd>ctrl / cmd + k</kbd>
        </div>
        <div class="header-right">
            <span class="top-system-label">PGDAS-D · Anexo I</span>
            <span class="status-pill"><span class="status-dot"></span> Online</span>
            <a class="top-outlined" href="/?page=sobre"><?= $symbol('info') ?> Sobre</a>
            <a class="icon-btn top-help" href="/?page=privacidade" title="Privacidade"><?= $symbol('lock') ?></a>
            <div class="user-area"><span class="user-avatar">NF</span><div class="user-name"><b>Acesso público</b><span>Sem cadastro necessário</span></div><?= $symbol('chevron') ?></div>
        </div>
    </header>
    <div id="mobileOverlay" class="mobile-overlay" hidden></div>
    <aside id="sidebar" class="sidebar">
        <div class="sidebar-scroll">
            <a data-menu="painel" class="menu-link <?= $page === 'painel' ? 'active' : '' ?>" href="<?= $nav('painel') ?>"><?= $symbol('grid') ?><span>Painel</span></a>
            <div class="menu-group-label">FERRAMENTAS FISCAIS</div>
            <a data-menu="apuracao" class="menu-link <?= $page === 'apuracao' ? 'active' : '' ?>" href="<?= $nav('apuracao') ?>"><?= $symbol('calculator') ?><span>Nova apuração</span></a>
            <a data-menu="historico" class="menu-link <?= $page === 'historico' ? 'active' : '' ?>" href="<?= $nav('historico') ?>"><?= $symbol('history') ?><span>Histórico local</span></a>
            <a data-menu="conferencia" class="menu-link <?= $page === 'conferencia' ? 'active' : '' ?>" href="<?= $nav('conferencia') ?>"><?= $symbol('file') ?><span>Conferência de XML</span></a>
            <div class="menu-group-label">REFERÊNCIAS</div>
            <a data-menu="regras" class="menu-link <?= $page === 'regras' ? 'active' : '' ?>" href="<?= $nav('regras') ?>"><?= $symbol('book') ?><span>Regras tributárias</span></a>
            <div class="menu-group-label">PLATAFORMA</div>
            <a data-menu="privacidade" class="menu-link <?= $page === 'privacidade' ? 'active' : '' ?>" href="<?= $nav('privacidade') ?>"><?= $symbol('lock') ?><span>Privacidade</span></a>
            <a data-menu="sobre" class="menu-link <?= $page === 'sobre' ? 'active' : '' ?>" href="<?= $nav('sobre') ?>"><?= $symbol('help') ?><span>Sobre o projeto</span></a>
        </div>
        <div class="sidebar-bottom">
            <div class="sidebar-privacy"><div class="privacy-icon"><?= $symbol('shield') ?></div><div><strong>Seus dados são seus.</strong><span>Nenhum histórico fiscal é salvo em nossos servidores.</span></div></div>
            <div class="sidebar-version">NORTHFIELD <span>v0.1.0 • PHP 8.4</span></div>
        </div>
    </aside>
    <main id="mainContent" class="main-content" tabindex="-1">
        <div class="content-inner">
            <div class="breadcrumb"><a href="/?page=painel">PAINEL</a><span>/</span><?= $esc($upper($bread === 'Painel' ? 'VISÃO GERAL' : $bread)) ?></div>
            <div class="page-titlebar">
                <div><h1><?= $esc($pageTitle) ?></h1><p><?= $esc($pageDescription) ?></p></div>
                <div class="title-actions">
                <?php if ($page === 'painel' || $page === 'historico'): ?>
                    <a class="btn btn-outline" href="/?page=regras"><?= $symbol('book') ?> Base normativa</a>
                    <a class="btn btn-primary" href="/?page=apuracao"><?= $symbol('plus') ?> Nova apuração</a>
                <?php elseif ($page === 'apuracao'): ?>
                    <a class="btn btn-outline" href="/?page=historico"><?= $symbol('history') ?> Histórico</a>
                <?php elseif ($page === 'conferencia'): ?>
                    <a class="btn btn-outline" href="/?page=apuracao"><?= $symbol('calculator') ?> Apuração</a>
                <?php endif; ?>
                </div>
            </div>
            <?php require __DIR__ . '/pages/' . $page . '.php'; ?>
            <footer class="page-footer"><span>Northfield • projeto aberto para conferência tributária</span><span>Sem transmissão oficial • Sem guarda de documentos no servidor</span></footer>
        </div>
    </main>
    <div id="toastBox" class="toast-box" role="status" aria-live="polite"></div>
    <dialog id="passwordDialog" class="app-dialog"><form method="dialog" id="passwordForm"><div class="dialog-heading"><h2 id="passwordTitle">Backup protegido</h2><button type="button" id="passwordClose" class="icon-btn dark-icon" aria-label="Fechar"><?= $symbol('x') ?></button></div><p id="passwordHelp">Informe uma senha para proteger este arquivo.</p><label class="field"><span>Senha</span><input type="password" id="passwordInput" autocomplete="new-password" minlength="10" required placeholder="No mínimo 10 caracteres"></label><p class="field-hint">Se perder a senha, o arquivo não poderá ser recuperado.</p><div class="dialog-actions"><button type="button" id="passwordCancel" class="btn btn-outline">Cancelar</button><button type="submit" class="btn btn-primary" id="passwordConfirm">Confirmar</button></div></form></dialog>
<dialog id="confirmDialog" class="app-dialog"><div class="confirm-content"><h2>Confirmação</h2><p id="confirmText"></p><div class="dialog-actions"><button type="button" id="confirmNo" class="btn btn-outline">Cancelar</button><button type="button" id="confirmYes" class="btn btn-danger">Confirmar</button></div></div></dialog>
</div>
</body>
</html>
