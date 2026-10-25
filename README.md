# Products API — Clean Architecture + CQRS

A sample ASP.NET Core Web API built with .NET 8, Clean Architecture, CQRS, MediatR, EF Core, and SQL Server.

## Features
- `GET /api/products` — list products
- `GET /api/products/{id}` — get a product by ID
- `POST /api/products` — create a product
- Separate Domain, Application, Infrastructure, and API projects
- DTOs and request validation
- EF Core + SQL Server
- CQRS handlers dispatched through MediatR

## Project structure
```text
ProductsApiCleanArchitecture/
├── Products.sln
├── src/
│   ├── Products.Domain/
│   ├── Products.Application/
│   ├── Products.Infrastructure/
│   └── Products.API/
└── README.md
```

## Requirements
- .NET 8 SDK
- SQL Server or SQL Server LocalDB

## Configure the database
Edit `src/Products.API/appsettings.json` and update `ConnectionStrings:DefaultConnection` for your SQL Server instance.

Example for LocalDB:
```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ProductsDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

Example for SQL Server running locally:
```json
"DefaultConnection": "Server=localhost;Database=ProductsDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

For SQL authentication, use a suitable username/password locally, but do not commit real credentials to GitHub. Prefer User Secrets or environment variables.

## Create the database
From the repository root, run:
```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --project src/Products.Infrastructure --startup-project src/Products.API --output-dir Persistence/Migrations
dotnet ef database update --project src/Products.Infrastructure --startup-project src/Products.API
```

If `dotnet-ef` is already installed, skip the first command.

## Run the API
```bash
dotnet restore
dotnet run --project src/Products.API
```
Use the URL printed by ASP.NET Core. Swagger is enabled in Development.

## Try the endpoints

### Create a product
`POST /api/products`
```json
{
  "name": "Egyptian Cotton T-Shirt",
  "price": 350.00
}
```

A successful request returns `201 Created` with the created product representation.

### Get all products
`GET /api/products`

### Get one product
`GET /api/products/1`

A non-existent ID returns `404 Not Found`. Invalid input (blank name or price less than or equal to zero) returns `400 Bad Request`.

## Architecture notes
- **Domain** contains the `Product` entity and business invariant.
- **Application** contains DTOs, use-case requests/handlers, and the repository abstraction.
- **Infrastructure** implements persistence using EF Core and SQL Server.
- **API** handles HTTP, request validation, dependency registration, and endpoint responses.
- **CQRS** separates reads (queries) from writes (commands). This example uses one database; CQRS does not require separate databases.
- The repository abstraction is included to keep EF Core details outside the Application layer. For a small application, a repository can be unnecessary abstraction; choose based on project needs.

## Logging with Serilog

Serilog is configured in `src/Products.API` and uses `ILogger<T>` in application code.

- Console logs are written to the terminal.
- Daily rolling log files are written to `logs/log-YYYYMMDD.txt` relative to the API working directory.
- The file sink retains up to 7 rolling files.
- HTTP request summaries include method, path, status code, and elapsed time.
- Application logs use structured properties such as `{ProductId}` and `{ProductCount}`.

Run the API as usual:

```bash
dotnet restore
dotnet run --project src/Products.API
```

Do not log passwords, access tokens, payment details, or other sensitive data. For production deployments, configure centralized log storage and retention appropriate to your environment.

## Notes
This is an educational starter project. Before production use, add tests, centralized exception handling, authentication/authorization where required, structured logging, pagination, and secret management.
