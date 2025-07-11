# Finlytics Backend

Finlytics is a lightweight financial analytics platform designed for small businesses and startups to manage user profiles, daily financial records, and NPS tracking.  
This backend provides secure authentication, data APIs, and MongoDB persistence for the Finlytics web client.  
It is built using ASP.NET Core and MongoDB, following a clean layered architecture and best practices in API design, modular services, and token-based authentication.

## Features

- JWT authentication with BCrypt password hashing
- User registration and login
- Profile retrieval and updates
- MongoDB with Repository pattern
- Built-in Swagger API documentation

## Planned Improvements

- [ ] Role-based authorization
- [ ] Centralized error handling middleware
- [ ] Unit tests
- [ ] Docker support

## Technologies

- ASP.NET Core
- MongoDB
- JWT Authentication
- Swagger
- Layered Architecture (Application, Domain, Infrastructure, Presentation)

## How to Run

1. Configure MongoDB connection string in `appsettings.json`
2. Run the solution via Visual Studio or CLI:
   ```bash
   dotnet build
   dotnet run --project Finlytics.Server
   ```
3. Open Swagger UI at:
   ```
   https://localhost:<port>/swagger
   ```

## Project Structure

- `Application` - DTOs, Interfaces
- `Infrastructure` - MongoDB access, Services
- `Presentation` - Controllers (API)
- `Domain` - Entities

## API Endpoints Overview

| Method | Endpoint                | Description                          | Auth Required  |
|--------|-------------------------|--------------------------------------|----------------|
| GET    | /api/Company            | Get company data                     | ✅ Yes         |
| POST   | /api/Company            | Create new company record            | ✅ Yes         |
| GET    | /api/finance/get-data   | Retrieve finance data                | ✅ Yes         |
| POST   | /api/finance            | Submit financial data                | ✅ Yes         |
| POST   | /api/User/register      | Register a new user                  | ❌ No          |
| POST   | /api/User/login         | Login with email and password        | ❌ No          |
| GET    | /api/User/profile       | Get the authenticated user's profile | ✅ Yes         |
| PUT    | /api/User/profile       | Update user's profile data           | ✅ Yes         |