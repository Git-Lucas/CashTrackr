# CashTrackr

## 🧾 Problema Proposto

Um comerciante precisa controlar o seu fluxo de caixa diário, realizando lançamentos (débitos e créditos), e consultar relatórios com o saldo diário consolidado.

---

## ✅ Solução Adotada

### 1. Design

- Aplicação dos princípios SOLID com foco em:
  - S (Single Responsibility): responsabilidade única alinhada aos diferentes possíveis atores do sistema (regra de negócio por conformidade à alguma lei; alteração de estrutura do objeto apresentado ao front-end), conforme Robert C. Martin (Clean Architecture).
  - D (Dependency Inversion): alto nível desacoplado de baixo nível via interfaces e injeção de dependência.
- Aplicação de princípios defendidos por Domain Driven Design:
  - Aggregates e Aggregates Roots para modelagem.
  - Value Objects no lugar de tipos primitivos.
  - Domain Model orientado a comportamento (evitando Transaction Script).
  - Padrão Repository para abstração de persistência.

---

### 2. Arquitetura

- Padrão Clean Architecture: separação clara entre Camada de Domínio, Aplicação e Infraestrutura.
- Aplicação de CQRS:
  - Comandos: gravação no banco relacional (SQL Server).
  - Queries: leitura otimizada via Redis.
  - Eventos de domínio asseguram consistência entre leitura e escrita.

📐 Diagrama:
![](https://github.com/Git-Lucas/CashTrackr/blob/master/imgs/CashTrackr_Architecture.png)


---

### 3. Backend

- Linguagem: C# (.NET 8)
- API RESTful com os seguintes endpoints:

| Método | Rota                      | Descrição                          |
|--------|---------------------------|------------------------------------|
| POST   | /transactions             | Criação de lançamento (débito/crédito) |
| GET    | /balance?date=YYYY-MM-DD  | Consulta de saldo diário consolidado |

- Banco de Dados:
  - SQL Server (persistência de transações)
  - Redis (projeção de saldo diário)
- ORM: Entity Framework Core
- Testes:
  - xUnit (98,6% de cobertura)
  - Análise de cobertura e qualidade via SonarQube

---

### 4. Infraestrutura / DevOps

- Docker Compose com 3 containers:
  - API
  - SQL Server
  - Redis
- DevTools:
  - SonarQube integrado ao Visual Studio
  - Swagger para documentação da API

---

### 5. Execução

Pré-requisitos:
- Docker e Docker Compose instalados.

Para iniciar o ambiente:

```bash
docker-compose up --build
```

---

## ✅ Testes e Cobertura de Código

### Execução dos testes

Utilizando o .NET CLI, execute:

```bash
dotnet test --no-build --verbosity normal
```

## 🧪 Geração do Relatório de Cobertura

Para gerar o relatório com cobertura de código, utilize a ferramenta Coverlet integrada com o xUnit:

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutput=./TestResults/ /p:CoverletOutputFormat=opencover
```

O relatório será salvo no diretório `./TestResults/` no formato OpenCover.

Para visualizar o relatório de forma gráfica, utilize o ReportGenerator:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:./TestResults/coverage.opencover.xml -targetdir:./TestResults/CoverageReport
```

Abra o arquivo abaixo no navegador para acessar o relatório interativo:
```bash
./TestResults/CoverageReport/index.html
```

📈 Cobertura atual: 98,6%

---

## ▶️ Acesso Rápido

- Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- SQL Server: localhost:1433
- Redis: localhost:6379

---

## 📄 Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- SQL Server
- Redis
- xUnit
- Docker
- SonarQube
- Swagger

---

## 📚 Referências Técnicas

- Clean Architecture – Robert C. Martin
- Domain-Driven Design – Eric Evans
- CQRS Pattern – Microsoft Docs