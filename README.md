# 🌐 User Management API

A **RESTful User Management API** built with **ASP.NET Core 7.0**, demonstrating back-end development practices including **CRUD operations**, **input validation**, **middleware for logging, error handling, and authentication**, and an **in-memory data store**. This API is designed for internal tools such as HR or IT systems, providing a simple and extensible user management solution.

---

## 🎯 Objectives

This project was created to meet the following criteria:  

- Implement CRUD endpoints (`GET`, `POST`, `PUT`, `DELETE`) for managing users.  
- Apply middleware for **logging**, **error handling**, and **authentication**.  
- Include **input validation** to ensure only valid user data is processed.  
- Use an **in-memory data store** for simplicity and testing.  
- Provide clear instructions for running and testing the API.

---

## 🚀 Features
### ✅ API Endpoints & Validation
- **GET /api/users** – Retrieve all users (allowed anonymously for testing).  
- **GET /api/users/{id}** – Retrieve a user by ID.  
- **POST /api/users** – Add a new user with validation.  
- **PUT /api/users/{id}** – Update an existing user’s details.  
- **DELETE /api/users/{id}** – Remove a user by ID.  
- **DTOs** (`UserCreateDto`, `UserUpdateDto`) enforce validation and data integrity.  

### 🎨 Middleware
- **RequestLoggingMiddleware** – Logs HTTP method, request path, and response status code.  
- **ErrorHandlingMiddleware** – Catches unhandled exceptions and returns consistent JSON error responses.  
- **Token Authentication Middleware** – Uses a simple token scheme; protected endpoints require the header:  

---

## 🚧 Future Improvements
- Replace in-memory store with a persistent database.
- Implement JWT authentication for production readiness.
- Add role-based access control for fine-grained permissions.
- Integrate Swagger / OpenAPI documentation.
- Add unit tests with xUnit to verify controller and service functionality.

---

## 📜 License
This project is for educational purposes under the Coursera Microsoft Full Stack Developer assignment. You may modify and use this project for learning, but proper credit to the author is appreciated.
