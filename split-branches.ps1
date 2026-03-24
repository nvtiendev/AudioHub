################################################################################
# AudioHub - Split Branches Script (Fixed)
# 1) Restores local main from GitHub (origin/main)
# 2) Creates isolated backend branch  (backend code only)
# 3) Creates isolated frontend branch (frontend code only)
################################################################################

function Run-Git {
    param([string[]]$args_)
    & git @args_
    if ($LASTEXITCODE -ne 0) {
        throw "Git command failed: git $($args_ -join ' ')"
    }
}

$ErrorActionPreference = "Stop"
Set-Location (Split-Path -Parent $MyInvocation.MyCommand.Path)

Write-Host "===== AudioHub Branch Splitter (Fixed) =====" -ForegroundColor Cyan

# ── STEP 1: Restore local main from GitHub ───────────────────────────────────
Write-Host "`n[STEP 1] Fetching latest from GitHub..." -ForegroundColor Yellow
Run-Git "fetch", "origin"

Write-Host "         Restoring local main from origin/main..." -ForegroundColor Gray
Run-Git "checkout", "main"
Run-Git "reset", "--hard", "origin/main"
Write-Host "         main restored OK" -ForegroundColor Green

# ── STEP 2: Delete old broken branches (if they exist) ───────────────────────
Write-Host "`n[STEP 2] Cleaning up any existing local backend/frontend branches..." -ForegroundColor Yellow
foreach ($b in @("backend", "frontend")) {
    $exists = (git branch --list $b).Trim()
    if ($exists -ne "") {
        Write-Host "         Deleting local branch: $b" -ForegroundColor Gray
        Run-Git "branch", "-D", $b
    }
    # Also delete on remote if already there
    $remoteExists = (git ls-remote --heads origin $b) 
    if ($remoteExists -ne "") {
        Write-Host "         Deleting remote branch: origin/$b" -ForegroundColor Gray
        Run-Git "push", "origin", "--delete", $b
    }
}
Write-Host "         Cleanup done" -ForegroundColor Green

# ── STEP 3: Create BACKEND branch ────────────────────────────────────────────
Write-Host "`n[STEP 3] Creating 'backend' branch..." -ForegroundColor Yellow
Run-Git "checkout", "main"
Run-Git "checkout", "-b", "backend"

Write-Host "         Removing frontend files..." -ForegroundColor Gray
Run-Git "rm", "-rf", "audiohub-frontend/"
Run-Git "rm", "-f", "Dockerfile.frontend"
Run-Git "rm", "-f", "nginx.conf"

$readmeBackend = @"
# AudioHub - Backend

This branch contains the **backend** source code only.

## Tech Stack
- Framework : .NET 9.0 Web API
- Language  : C# 13
- Docs      : Swagger / OpenAPI

## Run locally
``````bash
cd audiohub-backend/AudioHub.API
dotnet run
``````
API: http://localhost:5000

## Docker
``````bash
docker build -f Dockerfile.backend -t audiohub-backend .
docker run -p 5000:5000 audiohub-backend
``````
"@
Set-Content -Path "README.md" -Value $readmeBackend -Encoding UTF8
Run-Git "add", "README.md"
Run-Git "commit", "-m", "chore: backend branch - keep backend code only"
Run-Git "push", "-u", "origin", "backend"
Write-Host "         backend branch pushed OK" -ForegroundColor Green

# ── STEP 4: Create FRONTEND branch ───────────────────────────────────────────
Write-Host "`n[STEP 4] Creating 'frontend' branch..." -ForegroundColor Yellow
Run-Git "checkout", "main"      # <-- back to FULL main before branching
Run-Git "checkout", "-b", "frontend"

Write-Host "         Removing backend files..." -ForegroundColor Gray
Run-Git "rm", "-rf", "audiohub-backend/"
Run-Git "rm", "-f", "Dockerfile.backend"

$readmeFrontend = @"
# AudioHub - Frontend

This branch contains the **frontend** source code only.

## Tech Stack
- Framework : React 18 (Vite)
- Language  : TypeScript
- Styling   : Vanilla CSS (Glassmorphism)
- Icons     : Lucide React
- Animation : Framer Motion

## Run locally
``````bash
cd audiohub-frontend
npm install
npm run dev
``````
App: http://localhost:3000

## Docker
``````bash
docker build -f Dockerfile.frontend -t audiohub-frontend .
docker run -p 3000:80 audiohub-frontend
``````
"@
Set-Content -Path "README.md" -Value $readmeFrontend -Encoding UTF8
Run-Git "add", "README.md"
Run-Git "commit", "-m", "chore: frontend branch - keep frontend code only"
Run-Git "push", "-u", "origin", "frontend"
Write-Host "         frontend branch pushed OK" -ForegroundColor Green

# ── STEP 5: Return to main ────────────────────────────────────────────────────
Write-Host "`n[STEP 5] Switching back to main..." -ForegroundColor Yellow
Run-Git "checkout", "main"

# ── Summary ───────────────────────────────────────────────────────────────────
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  All done! Branches on GitHub:" -ForegroundColor Cyan
Write-Host ""
Write-Host "  main     -> full monorepo (unchanged)" -ForegroundColor White
Write-Host "  backend  -> audiohub-backend/ only"    -ForegroundColor White
Write-Host "  frontend -> audiohub-frontend/ only"   -ForegroundColor White
Write-Host ""
Write-Host "  https://github.com/nvtiendev/AudioHub" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
