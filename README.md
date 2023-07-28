# WebApiAutores

![Estado del Proyecto](https://img.shields.io/badge/estado-en%20desarrollo-yellow)

## Descripción

Esta Web API proporciona un servicio para gestionar información sobre autores y sus libros. Permite realizar operaciones CRUD (crear, leer, actualizar y eliminar) tanto para autores como para libros y comentarios.

Esta web API contiene un registro de autores y sus libros además de comentarios a cada libro esta relación corresponde a una relación uno a muchos (cada libro se relaciona a una colección de comentarios y cada comentario se relaciona a un solo libro), cada libro se ha relacionado con sus respectivos autores y cada autor puede estar relacionada a muchos libros, es decir, una relación muchos a muchos.

## Requisitos Previos 

* .NET 6.0.
* Paquetes NuGet:
  * Microsoft.EntityFrameworkCore.SqlServer
  * Microsoft.EntityFrameworkCore.Tools
  * AutoMapper.Extensions.Microsoft.DependencyIn
* SQL Server 2019

## Instrucciones de Instalación

1. Clona el repositorio en tu equipo local.
2. Abre el proyecto en Visual Studio o en tu editor de código preferido.
3. Ejecuta el comando `update-database` para crear la base de datos en tu instancia local.
4. Ejecuta la aplicación si estás en VS o ejecuta el comando `dotnet run` para iniciar la aplicación si estás en otro IDE.

## Tecnologías Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI) para documentación de la API
