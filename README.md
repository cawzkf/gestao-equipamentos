# Gestão de Equipamentos — API REST (.NET + PostgreSQL)

Trabalho Final do Módulo 3 — Desenvolvimento em C#.
API REST em ASP.NET Core, com EF Core, PostgreSQL e autenticação, organizada em camadas.

## Stack

- .NET 10 / ASP.NET Core
- Entity Framework Core 10 + Npgsql (PostgreSQL)
- PostgreSQL 16 (via Docker)
- JWT Bearer Authentication
- BCrypt.Net (hash de senhas)
- Swashbuckle (Swagger/OpenAPI)

## Estrutura da solution

```
GestaoEquipamentos/
├── GestaoEquipamentos.sln
├── GestaoEquipamentos.API/            # Controllers, Swagger, auth, middlewares, Program.cs
├── GestaoEquipamentos.Application/    # DTOs, services, interfaces
├── GestaoEquipamentos.Domain/         # Entidades, enums
├── GestaoEquipamentos.Infrastructure/ # DbContext, repositories, migrations (EF Core)
└── GestaoEquipamentos.Exceptions/     # Exceções customizadas e middleware de erros
```

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

### 4. Rodar a API

```bash
dotnet run --project GestaoEquipamentos.API
```

O Swagger ficará disponível em `http://localhost:<porta>/swagger` (ambiente Development).

## Configuração

A connection string vem de fontes diferentes conforme o ambiente:

- **Desenvolvimento:** já vem pronta em `appsettings.Development.json` (banco local do docker-compose, porta 5433). Basta `dotnet run` — não precisa configurar nada.
- **Produção:** o `appsettings.json` base **não contém credenciais**. A connection string é lida de variável de ambiente. Copie `config/.env.example` para `config/.env.prod` (ignorado pelo git) e preencha:
  - `ConnectionStrings__DefaultConnection` — usada pela API.
  - `CONNECTION_STRING` — usada pelas migrations (`AppDbContextFactory`).

## Endpoints

### Autenticação — públicos

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/auth/register` | Cria um novo usuário e retorna o token JWT |
| POST | `/api/auth/login` | Autentica e retorna o token JWT |
| GET | `/api/auth/me` | Retorna dados do usuário autenticado 🔒 |

### Equipamentos 🔒

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/equipment` | Lista todos os equipamentos |
| GET | `/api/equipment?categoryId={id}` | Lista equipamentos filtrados por categoria |
| GET | `/api/equipment/{id}` | Busca equipamento por ID |
| POST | `/api/equipment` | Cria um novo equipamento |
| PUT | `/api/equipment/{id}` | Atualiza um equipamento |
| DELETE | `/api/equipment/{id}` | Remove um equipamento |

### Categorias 🔒

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/category` | Lista todas as categorias |
| GET | `/api/category/{id}` | Busca categoria por ID |
| POST | `/api/category` | Cria uma nova categoria |
| PUT | `/api/category/{id}` | Atualiza uma categoria |
| DELETE | `/api/category/{id}` | Remove uma categoria |

### Fornecedores 🔒

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/supplier` | Lista todos os fornecedores |
| GET | `/api/supplier/{id}` | Busca fornecedor por ID |
| POST | `/api/supplier` | Cria um novo fornecedor |
| PUT | `/api/supplier/{id}` | Atualiza um fornecedor |
| DELETE | `/api/supplier/{id}` | Remove um fornecedor |

### Histórico de Equipamentos 🔒

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/equipment-history` | Lista todos os registros de histórico |
| GET | `/api/equipment-history/{id}` | Busca registro por ID |
| GET | `/api/equipment/{id}/history` | Lista o histórico de um equipamento específico |
| POST | `/api/equipment-history` | Registra uma ação no histórico |
| DELETE | `/api/equipment-history/{id}` | Remove um registro do histórico |

> 🔒 Endpoints protegidos exigem o header `Authorization: Bearer {token}`.

## Autenticação

1. Crie um usuário via `POST /api/auth/register`
2. Faça login via `POST /api/auth/login` e copie o `token` da resposta
3. No Swagger, clique em **Authorize** e informe `Bearer {token}`
4. Todos os endpoints marcados com 🔒 estarão disponíveis

## Tratamento de erros

Todas as exceções são tratadas por um middleware global. Respostas de erro seguem o formato:

```json
{
  "message": "Equipamento com identificador '99' não foi encontrado."
}
```

| Situação | Status HTTP |
|----------|-------------|
| Recurso não encontrado | 404 |
| Conflito (ex: e-mail duplicado) | 409 |
| Credenciais inválidas | 401 |
| Erro de validação | 400 |
| Erro interno | 500 |
