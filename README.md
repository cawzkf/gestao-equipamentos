# Gestão de Equipamentos — API REST (.NET + PostgreSQL)

Trabalho Final do Módulo 3 — Desenvolvimento em C#.
API REST em ASP.NET Core, com EF Core, PostgreSQL e autenticação, organizada em camadas.

## Stack

- .NET 10 / ASP.NET Core
- Entity Framework Core 10 + Npgsql (PostgreSQL)
- PostgreSQL 16 (via Docker)

## Estrutura da solution

```
GestaoEquipamentos/
├── GestaoEquipamentos.sln
├── GestaoEquipamentos.API/            # Controllers, Swagger, auth, Program.cs
├── GestaoEquipamentos.Application/     # DTOs, services, interfaces, casos de uso
├── GestaoEquipamentos.Domain/          # Entidades, enums, regras essenciais
├── GestaoEquipamentos.Infrastructure/  # DbContext, repositories, migrations (EF Core)
├── GestaoEquipamentos.Exceptions/      # Exceções customizadas e padronização de erros
└── docker-compose.yml                  # PostgreSQL
```

> Divisão de tarefas, modelagem das entidades e próximos passos estão em **[PLANO.pdf](PLANO.pdf)**.

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (para o PostgreSQL)
- Ferramenta `dotnet-ef`:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

## Como rodar

### 1. Subir o banco PostgreSQL

```bash
docker compose up -d
```

Banco padrão (definido no `docker-compose.yml` e na connection string):

| Parâmetro | Valor |
|-----------|-------|
| Host | localhost |
| Porta | 5433 |
| Database | gestao_equipamentos |
| Usuário | postgres |
| Senha | postgres |

> A porta do host é **5433** (a 5432 costuma estar ocupada por um Postgres local). Dentro do container continua 5432.

### 2. Restaurar e compilar

```bash
dotnet build
```

### 3. Aplicar as migrations

As migrations já estão no repositório. Para criar as tabelas no banco, basta aplicá-las:

```bash
dotnet ef database update --project GestaoEquipamentos.Infrastructure
```

Para gerar uma nova migration depois de mudar entidades ou configurations:

```bash
dotnet ef migrations add NomeDaMigration --project GestaoEquipamentos.Infrastructure
```

Há uma `AppDbContextFactory` que permite rodar esses comandos sem subir a API.

Migrations já aplicadas: `InitialCreate`, `ConfigureEquipment`,
`ConfigureRemainingEntities`, `AddEquipmentHistoryForeignKey`.

### 4. Rodar a API

```bash
dotnet run --project GestaoEquipamentos.API
```

O Swagger ficará disponível em `https://localhost:<porta>/swagger` (ambiente Development).

## Configuração

A connection string vem de fontes diferentes conforme o ambiente:

- **Desenvolvimento:** já vem pronta em `appsettings.Development.json` (banco local do
  docker-compose, porta 5433). Basta `dotnet run` — não precisa configurar nada.
- **Produção:** o `appsettings.json` base **não contém credenciais**. A connection string
  é lida de variável de ambiente. Copie `config/.env.example` para `config/.env.prod`
  (ignorado pelo git) e preencha:
  - `ConnectionStrings__DefaultConnection` — usada pela API.
  - `CONNECTION_STRING` — usada pelas migrations (`AppDbContextFactory`).

Para carregar o `config/.env.prod` antes de rodar (PowerShell):

```powershell
Get-Content config/.env.prod | Where-Object { $_ -match '=' -and $_ -notmatch '^#' } | ForEach-Object {
    $k, $v = $_ -split '=', 2
    [Environment]::SetEnvironmentVariable($k, $v)
}
dotnet run --project GestaoEquipamentos.API
```

A camada de Infra é registrada via `builder.Services.AddInfrastructure(builder.Configuration)`.

## Status

Camada Infrastructure concluída: repositórios, DbContext/DbSets, configurations,
relacionamentos e migrations aplicadas. Demais camadas em desenvolvimento —
ver progresso e responsáveis em **[PLANO.pdf](PLANO.pdf)**.
