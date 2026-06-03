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

Controller → Service → Repository → Banco de Dados

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

LocalOrbital (1) → (N) EstufaConfig

EstufaConfig (1) → (N) LeituraSensor

EstufaConfig (1) → (N) AlertaEstufa

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

Controllers

Services

Repositories

Interfaces

Models

DTOs

Enums

Data

Migrations

---

# Como Executar

## Restaurar Dependências

dotnet restore

## Executar Migrations

dotnet ef database update

## Executar Projeto

dotnet run

---

# Swagger

Após iniciar a aplicação:

[https://localhost:xxxx/swagger](https://localhost:xxxx/swagger)

