# NetCore6-Redis-RestAPI

## Contenido
1. [Requerimientos & Crear projecto](#requerimientos-&-crear-projecto)
1. [Correr projecto](#correr-projecto)
1. [Tecnologías utilizadas](#tec-util)
1. [Schema](#schema)

## Requerimientos & Crear projecto

Rest API usando open source database Redis,tomar en cuenta que se tiene que tener instalado Net Core 6 y Redis (tanto localmente o en un contenedor docker)

 1. Crear net core webapi `dotnet new webapi -n RedisAPI`
 2. Instalar nuget package: `dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis --version 6.0.4`
 3. Crear docker image usando docker compose: `docker-compose up -d`

## Correr projecto

- dotnet build
- dotnet run

## Tecnologias utilizadas

- Net Core 6
- Docker
- Redis

## Schema

**Platform**

- string Id
- string Name
