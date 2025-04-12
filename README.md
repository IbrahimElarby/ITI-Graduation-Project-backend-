# 🍽️ ITI Graduation Project Backend

This is the backend API for our full-stack ITI Graduation Project. It supports user authentication, recipe management, blog posts, comments, categories, and admin operations.

---

## 🚀 Technologies Used

- ASP.NET Core 7
- Entity Framework Core
- SQL Server
- JWT Authentication
- Repository + Unit of Work Pattern
- Swagger (OpenAPI)
- Serilog for Logging
- FluentValidation (coming soon)

---

## 📦 Project Structure

```plaintext
├── DL (Data Layer)
│   ├── Context (DbContext)
│   ├── Models (Entities)
│   └── Repositories
├── BL (Business Layer)
│   ├── Services (Managers)
│   └── DTOs
├── API (Controllers)
├── Shared (GeneralResult, Error Models)
