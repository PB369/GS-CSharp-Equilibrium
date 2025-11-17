# Documentação API Equilibrium

Este é um projeto de uma API RESTful desenvolvida para uma solução que ajuda pessoas a conciliar vida pessoal e profissional em regimes de trabalho híbrido. A API permite registrar e modificar usuários e agendamentos de trabalho, indicando horários e modelo de trabalho (Presencial ou Remoto).

## 💎 Integrantes do Grupo

- RM97937 | Pedro Henrique Fernandes Lô de Barros
- RM97824 | Vinicius Oliveira de Barros

## 🎬 Link do Vídeo Demonstrativo

https://www.youtube.com/watch?v=BuYWImdJdyU

## 🧠 Tecnologias Usadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Migrations
- Swagger
- Banco de Dados MySQL

## 📁 Estrutura do Projeto

```bash
Equilibrium.Api/
 ├── Controllers/
  └── v1/
 ├── Data/
 ├── DTOs/
 ├── Middleware/
 ├── Models/
 ├── Services/
 ├── appsettings.json
 └── Program.cs
```

## 📜 Como executar o projeto

Siga os passos abaixo para rodar o projeto localmente:

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina:

- Visual Studio
- .NET 8
- MySQL 8

### Passo a passo

```bash
1. Clone este projeto e abra-o no Visual Studio
2. Configure a connection string em appsettings.json com suas credenciais do MySQL (usuário e senha)
3. No terminal do projeto no Visual Studio, execute:
    dotnet add package Microsoft.EntityFrameworkCore --version 8.*
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.*
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.*
    dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.*
    dotnet add package Microsoft.AspNetCore.Mvc.Versioning --version 5.1.0
    dotnet add package Swashbuckle.AspNetCore --version 6.*
4. Depois, ainda no terminal, execute:
* dotnet tool install --global dotnet-ef --version 8.0.10
* dotnet ef migrations add InitialCreate
* dotnet ef database update
4. Pressione f5 para iniciar o projeto

OBS: Caso seja solicitado a instalação de certificado autoassinado, aceite a instalação.
```

## 📗 Documentação Swagger

## ✒️ Fluxo de Arquitetura
