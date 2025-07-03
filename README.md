# SalesApp
Full-stack Sale Application

---

Built with:

- .NET 9
- SignalR
- React + Vite (TypeScript)
- PostgreSQL 15
- NGINX
- Docker

Features:

Admin panel to manage stock  
Checkout flow with cash & change calculation  
Responsive UI  
API documented with Swagger  
Full dockerized build & run  
Database seeding for demo items

---

## Setup

### Prerequisites:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js 20+](https://nodejs.org/)
- [Docker + Docker Compose](https://www.docker.com/)

---

### Running the app

- Clone the repo
- Run
```bash
docker compose -f docker-compose.full.yml up --build
```
First time run might not launch server so from docker just start it again.
- Access swagger from http://localhost:8080/swagger
- Access the app from http://localhost:5173/

### Local Development with Visual Studio
Navigate to the repo folder and run

```bash
cd salesapp.client
npm install
```
```bash
docker compose up -d db
```

Then configure startup projects in VS
- Create a profile where Servers debug target is IIS Express
- Client and Server action is Start 

Then press start
