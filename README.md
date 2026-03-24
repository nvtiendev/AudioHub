# AudioHub - Frontend

This branch contains the **frontend** source code for AudioHub.

## Tech Stack
- Framework : React 18 (Vite)
- Language  : TypeScript
- Styling   : Vanilla CSS (Glassmorphism)
- Icons     : Lucide React
- Animation : Framer Motion

## Getting Started
```bash
cd audiohub-frontend
npm install
npm run dev
```
App runs at: http://localhost:3000

## Docker
```bash
docker build -f Dockerfile.frontend -t audiohub-frontend .
docker run -p 3000:3000 audiohub-frontend
```
