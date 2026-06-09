# Diagrama de Fluxo - GlobalSoluction

```text

Usuário
  ↓
Swagger / API REST
  ↓
Autenticação JWT
  ↓
Cadastro de Local Orbital
  ↓
Cadastro de Estufa
  ↓
Envio de Leitura Ambiental Simulada
  ↓
Validação dos Parâmetros Ideais da Estufa
  ↓
A leitura está dentro do intervalo ideal?
  ↓
 ┌───────────────┬────────────────┐
 │ Sim           │ Não            │
 ↓               ↓
Salva leitura    Salva leitura
sem alerta       e gera alerta automático
 │               │
 └───────┬───────┘
         ↓
Consulta de Alertas
         ↓
Resolução de Alertas
         ↓
Relatório Resumo / Exportação JSON
         ↓
Banco de Dados MySQL

```text