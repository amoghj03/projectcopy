# Bank Employee Management API - .NET 9 Backend

A modern ASP.NET Core 9.0 Web API with PostgreSQL database connection for the Bank Employee Management System.

## 🚀 Technology Stack

- **.NET 9.0** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core 9.0** - ORM for database operations
- **PostgreSQL** - Relational database
- **Npgsql** - PostgreSQL driver for .NET
- **Swagger/OpenAPI** - API documentation

## 📋 Prerequisites

Before running this project, make sure you have:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed
- [PostgreSQL](https://www.postgresql.org/download/) 12 or higher installed
- Your favorite IDE (Visual Studio 2022, VS Code, or JetBrains Rider)

## 🔧 Setup Instructions

### 1. Navigate to Project Directory

```bash
cd backend
```

### 2. Configure Database Connection

Open `appsettings.json` and update the PostgreSQL connection string with your credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=BankEmployeeDB;Username=your_username;Password=your_password"
  }
}
```

### 3. Install Dependencies

```bash
dotnet restore
```

### 4. Create PostgreSQL Database

Connect to PostgreSQL and create the database:

```sql
CREATE DATABASE BankEmployeeDB;
```

Or use the development database:

```sql
CREATE DATABASE BankEmployeeDB_Dev;
```

### 5. Run Entity Framework Migrations

Once you've created your models, run:

```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Update database with migrations
dotnet ef database update
```

### 6. Run the Application

```bash
dotnet run
```

Or use the watch mode for development:

```bash
dotnet watch run
```

The API will start on:

- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger UI**: https://localhost:5001/swagger

## 📁 Project Structure

```
backend/
├── Controllers/            # API controllers (endpoints)
├── Data/                  # Database context
│   └── ApplicationDbContext.cs
├── Models/                # Entity models
├── Services/              # Business logic layer
├── Properties/            # Launch settings
│   └── launchSettings.json
├── Program.cs             # Application entry point
├── appsettings.json       # Configuration settings
├── appsettings.Development.json
├── BankAPI.csproj         # Project file
└── README.md
```

## 🗄️ Database Setup

### Entity Framework Core Commands

```bash
# Add a migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# List all migrations
dotnet ef migrations list

# Generate SQL script
dotnet ef migrations script
```

## 🔐 Configuration

### appsettings.json

- **ConnectionStrings**: Database connection configurations
- **Logging**: Log levels for different components
- **AllowedHosts**: CORS allowed hosts

### appsettings.Development.json

Development-specific settings with more detailed logging.

## 🌐 CORS Configuration

The API is configured to allow requests from:

- Frontend: http://localhost:3000

Modify the CORS policy in `Program.cs` to add more origins if needed.

## 📦 Installed NuGet Packages

- **Npgsql.EntityFrameworkCore.PostgreSQL** (9.0.1) - PostgreSQL provider
- **Microsoft.EntityFrameworkCore.Design** (9.0.0) - EF Core design-time components
- **Microsoft.EntityFrameworkCore.Tools** (9.0.0) - EF Core CLI tools
- **Swashbuckle.AspNetCore** (7.2.0) - Swagger/OpenAPI support
- **Microsoft.AspNetCore.Authentication.JwtBearer** (9.0.0) - JWT authentication
- **Microsoft.AspNetCore.Cors** (2.2.0) - CORS support

## 🛠️ Development Workflow

### 1. Create Models

Add entity classes to the `Models/` folder:

```csharp
namespace BankAPI.Models;

public class Employee
{
    public int Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}
```

### 2. Update DbContext

Add DbSet properties to `Data/ApplicationDbContext.cs`:

```csharp
public DbSet<Employee> Employees { get; set; }
```

### 3. Create Migration

```bash
dotnet ef migrations add AddEmployee
```

### 4. Update Database

```bash
dotnet ef database update
```

### 5. Create Controllers

Add API controllers to the `Controllers/` folder to expose endpoints.

## 🔍 Testing the API

### Using Swagger UI

Navigate to https://localhost:5001/swagger to test API endpoints interactively.

### Using curl

```bash
# Example GET request
curl -X GET "https://localhost:5001/api/employees" -k
```

### Using PowerShell

```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/employees" -SkipCertificateCheck
```

## 🚨 Troubleshooting

### Connection Issues

1. Verify PostgreSQL is running:

   ```bash
   # Windows (PowerShell)
   Get-Service postgresql*

   # Or test connection
   psql -U postgres -c "SELECT version();"
   ```

2. Check connection string format and credentials

3. Ensure PostgreSQL accepts connections (check pg_hba.conf)

### Port Already in Use

If ports 5000/5001 are in use, modify `Properties/launchSettings.json`

### Migration Issues

```bash
# Drop database and recreate (development only!)
dotnet ef database drop
dotnet ef database update
```

## 📚 Next Steps

1. ✅ Basic setup complete
2. 📝 Define your entity models in the `Models/` folder
3. 🔄 Update `ApplicationDbContext.cs` with DbSets
4. 🗄️ Run migrations to create database schema
5. 🎯 Create controllers for API endpoints
6. 💼 Implement business logic in services
7. 🔐 Add authentication and authorization
8. ✅ Write unit and integration tests

## 🔗 Useful Links

- [.NET 9 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [Npgsql Documentation](https://www.npgsql.org/doc/)
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)

---

**Note**: This is a basic setup. No API endpoints have been created yet. Follow the development workflow to add your business logic and endpoints.
