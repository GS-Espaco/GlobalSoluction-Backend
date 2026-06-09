# GlobalSoluction

## Integrantes

* André de Sousa Neves – RM 553515
* Caio Sato Tominaga – RM 553633
* Eduardo Brites Coutinho – RM 552943
* Isabela Barcellos – RM 553746
* Thaís Gonçalves Leoncio – RM 553892

---

# Sobre o Projeto

O GlobalSoluction é uma API REST desenvolvida em .NET 8 com Entity Framework Core e MySQL para apoiar a gestão de ambientes agrícolas em contextos espaciais.

A solução foi inspirada no conceito de agricultura espacial, permitindo o cadastro de locais orbitais adequados para cultivo, configuração de estufas, recebimento de leituras ambientais simuladas, geração automática de alertas e emissão de relatórios operacionais.

O projeto foi desenvolvido para atender simultaneamente aos requisitos das disciplinas:

* Global Solution – C#
* SOA e WebServices

Utilizando um único repositório e uma única aplicação.

---

# Relação com a Global Solution

O GlobalSoluction representa o módulo responsável pelo monitoramento e gestão de ambientes agrícolas espaciais.

Dentro da proposta da Global Solution, a API auxilia na seleção de locais orbitais adequados para cultivo, configuração de estufas, monitoramento das condições ambientais e geração automática de alertas operacionais.

Dessa forma, a solução contribui para a sustentabilidade de missões espaciais de longa duração por meio da produção controlada de alimentos.

---

# ODS Relacionado

## ODS 2 – Fome Zero e Agricultura Sustentável

A solução contribui para pesquisas relacionadas à produção sustentável de alimentos em ambientes espaciais por meio do monitoramento de parâmetros ambientais necessários ao cultivo.

## ODS 9 – Indústria, Inovação e Infraestrutura

O projeto utiliza tecnologias modernas de desenvolvimento de software para apoiar iniciativas relacionadas à indústria espacial e sustentabilidade.

---

# Problema

Ambientes espaciais apresentam desafios relacionados a:

* Radiação
* Disponibilidade de recursos
* Controle climático
* Produção sustentável de alimentos

O sistema busca auxiliar no monitoramento dessas condições através de leituras ambientais e alertas automáticos.

---

# Arquitetura da Solução

A aplicação foi construída seguindo uma arquitetura em camadas:

```text
Controller → Service → Repository → Banco de Dados
```

Utilizando:

* DTOs
* Interfaces
* Injeção de Dependência
* Entity Framework Core
* MySQL

---

# Tecnologias Utilizadas

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* Swagger
* JWT Authentication
* BCrypt
* Dependency Injection

---

# Pacotes Utilizados

* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Design
* Pomelo.EntityFrameworkCore.MySql
* Microsoft.AspNetCore.Authentication.JwtBearer
* BCrypt.Net-Next
* Swashbuckle.AspNetCore
* Microsoft.OpenApi

---

# Modelagem do Domínio

## LocalOrbital

Representa regiões espaciais adequadas para instalação de estufas.

### Principais atributos

* Nome
* Planeta
* Região
* Proteção Natural
* Incidência Solar
* Presença de Gelo Subterrâneo
* Nível de Risco de Radiação

---

## EstufaConfig

Representa a configuração operacional de uma estufa.

### Principais atributos

* Tipo de Plantação
* Temperatura Ideal
* Umidade Ideal do Ar
* Umidade Ideal do Solo
* Luminosidade Ideal
* CO₂ Ideal

---

## LeituraSensor

Representa leituras ambientais recebidas pelo sistema.

### Principais atributos

* TipoSensor
* Valor
* DataLeitura

---

## AlertaEstufa

Representa alertas gerados automaticamente quando uma leitura está fora dos parâmetros ideais.

### Principais atributos

* TipoAlerta
* NivelCriticidade
* Mensagem
* Recomendacao
* Resolvido

---

## Usuario

Responsável pela autenticação e autorização da API.

### Principais atributos

* Nome
* Email
* SenhaHash
* Role

---

# Relacionamentos

```text
LocalOrbital (1) → (N) EstufaConfig

EstufaConfig (1) → (N) LeituraSensor

EstufaConfig (1) → (N) AlertaEstufa
```

---

# Fluxo da Aplicação

1. Cadastro de Local Orbital
2. Cadastro de Estufa
3. Recebimento de Leituras
4. Validação dos parâmetros
5. Geração automática de Alertas
6. Consulta de Alertas
7. Resolução de Alertas
8. Geração de Relatórios

---

# Funcionalidades

## Local Orbital

* GET
* GET BY ID
* POST
* PUT
* DELETE

## Estufa

* GET
* GET BY ID
* POST
* PUT
* DELETE

## Leituras

* POST
* GET
* GET POR ESTUFA

## Alertas

* GET
* GET POR ESTUFA
* PATCH RESOLVER

## Relatórios

* Resumo Operacional
* Exportação JSON

## Segurança

* Registro de Usuários
* Login
* JWT
* BCrypt

---

# Estrutura de Pastas

```text
Controllers
Services
Repositories
Interfaces
Models
DTOs
Enums
Data
Migrations
```

---

# Configuração do Banco de Dados

Criar o banco:

```sql
CREATE DATABASE GlobalSoluctionDb;
```

Configurar a connection string em:

```text
appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=GlobalSoluctionDb;user=root;password=123456"
  }
}
```

---

# Como Executar

## Restaurar Dependências

```bash
dotnet restore
```

## Executar Migrations

```bash
dotnet ef database update
```

## Executar Projeto

```bash
dotnet run
```

---

# Migrations

Criar migration:

```bash
dotnet ef migrations add InitialCreate
```

Aplicar migration:

```bash
dotnet ef database update
```

Remover migration:

```bash
dotnet ef migrations remove
```

---

# Swagger

Após iniciar a aplicação:

```text
https://localhost:xxxx/swagger
```

---

# Decisões Técnicas

As principais decisões técnicas adotadas foram:

* Utilização de ASP.NET Core Web API como interface navegável.
* Persistência de dados utilizando Entity Framework Core e MySQL.
* Arquitetura em camadas (Controller → Service → Repository).
* Uso de DTOs para transporte de dados.
* Autenticação baseada em JWT.
* Criptografia de senhas utilizando BCrypt.
* Geração automática de alertas a partir das leituras ambientais simuladas.
* Exportação de dados em JSON para atender aos requisitos da disciplina.

---
# Documentação Complementar

- [Diagrama de Fluxo](Docs/diagrama-fluxo.md)
- [Evidências de Execução](Docs/evidencias-execucao.md)