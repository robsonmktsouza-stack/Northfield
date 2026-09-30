# Arquitetura de proteção de dados

- **Navegador:** nome opcional de empresa, importação temporária de XML, histórico opt-in IndexedDB, criptografia/descriptografia de backups Web Crypto API.
- **Servidor:** recebe apenas números, competência e confirmações para calcular, responde JSON e descarta após requisição.
- **Sem armazenamento central:** não há tabelas de clientes, cadastro, registros de XML ou arquivos de relatório no servidor.
- **Hospedagem:** IP e metadados podem aparecer em logs técnicos de Nginx/hosting, com retenção mínima; não registre corpos de requisição, URLs com dados fiscais, dumps ou cabeçalhos sensíveis.
- **Segurança:** implantar HTTPS, atualizações regulares, rate limiting, monitoramento, restrição de diretórios, backup de regras e app, recuperação de incidentes.
- **Dispositivos compartilhados:** IndexedDB pode ser acessado pelo perfil local do navegador; usuário pode limpar na página Privacidade.
- **Criptografia portátil:** AES-GCM com PBKDF2-SHA256 250 mil iterações e sal aleatório. Não há recuperação de senha. Executada no navegador.
- **Responsabilidade:** disponibilizar termos/aviso de privacidade adequados à implantação e ajustar à LGPD antes da disponibilização pública.
