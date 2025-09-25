**This project is still getting updates! Therefore, keep in mind that README will also change in the future.**
# GoalTracker

**GoalTracker** is a production-ready ASP.NET Core Web API designed to help users create, track, and manage their daily goals.  
The project follows **Clean Architecture** and **Domain-Driven Design (DDD)** principles to ensure scalability, maintainability, and clear separation of concerns.

---

## ✨ Features

- **User Authentication & Authorization**
  - Secure registration and login via **ASP.NET Identity**
  - **JWT-based authentication** for API access
  - Role-based authorization

- **Goal Management**
  - Create, update, and delete daily goals
  - Retrieve current and past goals
  - Track progress over time

- **API Documentation**
  - Integrated **Swagger** for endpoint exploration
  - Postman collection included for testing

- **Error Handling & Validation**
  - Centralized exception middleware
  - Model validation with clear error responses

- **Testing**
  - **xUnit** test project
  - Unit tests for core logic

---

## 🏗️ Architecture

The project is made for studying it and structured around Clean Architecture with clear separation between layers:

- **Domain**  
  Core business logic, entities, and value objects

- **Application**  
  Use cases, service interfaces, DTOs, validators

- **Infrastructure**  
  EF Core persistence, repository implementations, Identity, JWT handling

- **API**  
  Controllers, request/response models, Swagger setup, middleware

- **Tests**  
  Unit and integration tests for reliability

---

## 🛠️ Tech Stack

- **.NET 8** — ASP.NET Core Web API  
- **Entity Framework Core** — Database access (SQL Server / InMemory)  
- **ASP.NET Identity** — Authentication & user management  
- **JWT (JSON Web Tokens)** — Secure stateless authentication  
- **xUnit** — Unit & integration testing  
- **Swagger / Postman** — API documentation and testing

---

## 🚀 Production-Readiness

- Authentication & Authorization implemented with **Identity + JWT**  
- Centralized **error handling middleware**  
- Separation of **read/write models** (DTOs)  
- Configurable persistence (**SQL Server** or **InMemory**)  
- Test project with growing coverage  
- Extendable architecture for scaling and additional features

---

