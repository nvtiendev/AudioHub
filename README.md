# AudioHub 🎵

AudioHub is a professional, full-stack audio streaming and utility platform. It provides a seamless interface for searching, streaming, and managing high-quality audio data. 

This project demonstrates a decoupling of legacy monolithic architectures into a modern **Microservices-ready** structure using **.NET 9 Web API** and **ReactJS (Vite)**.

## 🚀 Key Features

- **Full-Stack Architecture**: Clean separation between Backend (ASP.NET Core) and Frontend (React + TS).
- **Decoupled Core Logic**: Refactored business logic into a standalone Core library.
- **Proxy Download System**: Server-side proxying to ensure correct metadata and high-quality streaming.
- **VIP Handling**: Intelligent filtering and UI indicators for restricted content.
- **Batch Processing**: Support for album and playlist information retrieval.
- **Modern UI/UX**: Premium Glassmorphism design with responsive elements and smooth animations.
- **Containerized**: Ready-to-deploy with Docker and Docker Compose.

## 🛠️ Tech Stack

### Backend
- **Framework**: .NET 9.0 Web API
- **Core Logic**: C# 13, HttpClient, System.Text.Json
- **Documentation**: Swagger/OpenAPI

### Frontend
- **Framework**: React 18 (Vite)
- **Language**: TypeScript
- **Styling**: Vanilla CSS (Custom Glassmorphism)
- **Icons**: Lucide React
- **Animations**: Framer Motion

### DevOps
- **Containerization**: Docker, Docker Compose
- **Version Control**: Git

## 📦 Getting Started

### Prerequisites
- Docker & Docker Compose
- *Alternatively*: .NET 9 SDK and Node.js 20+

### Run with Docker (Recommended)
```bash
docker-compose up --build
```
- Frontend: `http://localhost:3000`
- Backend: `http://localhost:5000`

### Run Manually
1. **Backend**:
   ```bash
   cd audiohub-backend/AudioHub.API
   dotnet run
   ```
2. **Frontend**:
   ```bash
   cd audiohub-frontend
   npm install
   npm run dev
   ```

## 📝 License
This project is licensed under the MIT License.
