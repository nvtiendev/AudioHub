# AudioHub - Backend

This branch contains the **backend** source code for AudioHub.

## Tech Stack
- Framework : .NET 9.0 Web API
- Language  : C# 13
- Docs      : Swagger / OpenAPI

## Getting Started
```bash
cd audiohub-backend/AudioHub.API
dotnet run
```
API runs at: http://localhost:5000

## Docker
```bash
docker build -f Dockerfile.backend -t audiohub-backend .
docker run -p 5000:5000 audiohub-backend
```
