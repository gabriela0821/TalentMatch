# TalentMatch - Sistema de Emparejamiento de Empleo

## Descripción
Plataforma que conecta empleadores con candidatos mediante matching inteligente.

## Estructura
- `/Backend` - API REST en .NET 8
- `/Frontend` - Aplicación Blazor Server

## Requisitos
- .NET 8 SDK
- SQL Server
- Visual Studio 2022

## Instalación

### Backend
1. Restaurar BD: ejecutar scripts en `/Backend`
2. Configurar connection string en `appsettings.json`
3. Ejecutar: `dotnet run --project Backend/TalentMatch.API`

### Frontend
1. Configurar `ApiBaseUrl` en `appsettings.json`
2. Ejecutar: `dotnet run --project Frontend/TalentMatch.BlazorApp`

## Patrones y Arquitectura Implementados

### Clean Architecture
Separación en 4 capas independientes:
- **Domain:** Entidades de negocio puras (User, JobPosting, Application, etc.)
- **Application (Core):** Lógica de negocio, servicios, DTOs e interfaces
- **Infrastructure:** Acceso a datos, DbContext, configuraciones EF Core
- **API (Presentation):** Controllers, middleware, configuración

### Patrones de Diseño

**Repository Pattern**
- Abstracción del acceso a datos
- Interfaces para operaciones CRUD

**Dependency Injection**
- Inyección por constructor
- Registro centralizado de servicios
- Desacoplamiento de dependencias

**DTO Pattern**
- Separación entre entidades y objetos de transferencia
- Request/Response objects
- Protección del modelo de dominio

**Generic Response Pattern**
- Respuesta estandarizada `Response<T>`
- Manejo consistente de errores y mensajes

**Mapper Pattern (AutoMapper)**
- Mapeo automático DTO ↔ Entidad
- Perfiles configurables
- Reducción de código repetitivo

### Seguridad

**JWT (JSON Web Tokens)**
- Autenticación stateless
- Claims para roles (Employer/JobSeeker)
- Tokens con expiración configurable

**BCrypt**
- Hash seguro de contraseñas
- No almacenamiento en texto plano
