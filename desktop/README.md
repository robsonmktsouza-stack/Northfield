# Northfield Fiscal Desktop

Programa hospedeiro do motor do Simples Nacional. O desktop foi projetado para uso interno e para validar a futura biblioteca fiscal sem misturar regras tributárias com a interface.

## Stack

- C#
- .NET 10 LTS
- Windows Forms
- Sem dependências NuGet de terceiros
- Persistência local em JSON
- Importação de XML e ZIP
- Publicação `win-x64` self-contained em arquivo executável

## Abrir no Visual Studio

Abra:

```text
desktop/Northfield.Fiscal.Desktop.sln
```

Requisitos:

- Windows 10/11
- Visual Studio com workload **Desenvolvimento para desktop com .NET**
- SDK .NET 10

Pressione `F5` para executar.

## O que já funciona

- interface desktop contábil sem menu lateral;
- competência, empresa, RBT12, folha e cálculo visual do Fator R;
- importação múltipla de XML e ZIP por diálogo ou arrastar/soltar;
- leitura genérica de NFS-e por `LocalName`, sem depender de um namespace municipal específico;
- listagem e conferência dos documentos;
- sessão local automática e abertura/gravação de sessões `.northfield.json`;
- edição manual de classificação enquanto o motor não está conectado;
- segregação consolidada;
- espelho preparatório do PGDAS-D;
- memória de cálculo/auditoria;
- exportação CSV;
- impressão/preview do resumo;
- atalhos `Ctrl+I`, `Ctrl+S`, `F5` e `Delete`.

## Limite intencional

O aplicativo **não contém regras fiscais automáticas**. Isso é deliberado.

A interface depende de `IEngineGateway`. Hoje o `ManualEngineGateway` mantém o programa utilizável para importação, conferência e classificação manual. Quando a biblioteca do Simples estiver pronta, será criado um adapter que implementará essa interface e retornará as decisões ao desktop.

```text
Desktop -> FiscalContext/Documentos -> Biblioteca do Simples -> Decisões -> Desktop
```

Nenhum `if (serviço == ...)` de enquadramento tributário deve ser colocado na UI.

## Gerar o executável

Na raiz de `desktop`:

```bat
publish-win-x64.bat
```

O executável será gerado em:

```text
desktop/Northfield.Fiscal.Desktop/bin/Release/net10.0-windows/win-x64/publish/Northfield.Fiscal.Desktop.exe
```

O workflow `desktop-build.yml` também compila e publica automaticamente um artefato `Northfield-Fiscal-win-x64` no GitHub Actions.
