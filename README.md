# UserManagementAPI

This is a simple ASP.NET Core Web API demo implementing a User Management API with:
- CRUD endpoints (GET, POST, PUT, DELETE)
- Data validation (DataAnnotations)
- In-memory thread-safe user store
- Middleware: Error handling, Request logging, Token-based Authentication (demo)
- Designed to be used as a sample for assignments and testing with Postman

## How to run

Requirements: .NET 7 SDK

1. Extract the zip and open the folder.
2. Run `dotnet restore` then `dotnet run`.
3. The API will run on the default Kestrel URL (e.g., https://localhost:5001).
4. Swagger UI is available in Development mode at `/swagger`.

## Demo token for Authorization header
Use the header: `Authorization: Bearer demo-valid-token-123`
Note: For demo purposes, GET /api/users is allowed anonymously to list users.

## Files included
- Program.cs
- Controllers/UsersController.cs
- Models/User.cs
- DTOs/UserCreateDto.cs, UserUpdateDto.cs
- Services/IUserService.cs, UserService.cs
- Middlewares/ErrorHandlingMiddleware.cs, RequestLoggingMiddleware.cs, TokenAuthMiddleware.cs
