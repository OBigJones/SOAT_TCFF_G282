# SOAT_TCFF_G282
Software Architecture Tech Challenge Fast Food

# MCDONALDO

Sistema de gerenciamento de usuários e produtos para um restaurante fictício, desenvolvido em .NET 8.

## Visão Geral

Este projeto é composto por uma API RESTful que permite o cadastro e consulta de usuários, além do gerenciamento de produtos e controle de estoque. Utiliza arquitetura em camadas, separando responsabilidades entre Application, Domain, Infra e API.

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- Entity Framework Core
- Microsoft.Extensions.DependencyInjection
- Swagger (Swashbuckle)
- SQL Server (ou outro banco relacional via EF Core)

## Estrutura do Projeto

- **API**: Camada de apresentação (controllers, endpoints REST).
- **Application**: Serviços de aplicação, interfaces de repositórios, payloads e mapeamentos.
- **Domain**: Entidades de domínio e enums.
- **Infra**: Implementação dos repositórios, contexto de dados e adaptadores externos de pagamento.

## Como Executar

1. **Pré-requisitos**:
   - .NET 8 SDK instalado
   - SQL Server ou outro banco relacional configurado

2. **Configuração**:
   - Ajuste a string de conexão no `appsettings.json` do projeto API, se necessário.

3. **Build e Run**:
   - Para rodar o projeto é necessario a instalação do docker e docker-compose
   - rodar na linha de comando os seguintes comandos para executar o projeto pela primera vez.
   - na pasta raiz do projeto
   - docker compose up --build
   - o projeto será executado na porta 5024
   - http://localhost:5024/swagger/index.html
   - para rodar isso em vezes posteriores vc só precisará 
   - docker compose up

4. **Miro Event Storming DDD**
   - [DOCUMENTAÇÃO DDD MIRO](https://miro.com/app/board/uXjVIzpp7P0=/?share_link_id=391887046592)
