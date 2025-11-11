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
1. Restaurar BD: ejecutar scripts en `/Backend/Database`
2. Configurar connection string en `appsettings.json`
3. Ejecutar: `dotnet run --project Backend/TalentMatch.API`

### Frontend
1. Configurar `ApiBaseUrl` en `appsettings.json`
2. Ejecutar: `dotnet run --project Frontend/TalentMatch.BlazorApp`