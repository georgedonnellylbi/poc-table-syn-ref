# POCDatatables

## Overview
This project demonstrates cross-database relationships in Entity Framework Core using SQL Server synonyms. It includes:
- Two databases: `common` (for clubs) and `footage` (for films)
- A synonym in `footage` pointing to the `Clubs` table in `common`
- .NET 9 project with EF Core models and contexts

## Features
- Scaffolding for multiple databases
- Partial classes for custom Fluent API and navigation properties
- Sample data and schema evolution via SQL scripts
- LINQ queries with navigation properties using synonyms

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server (local or Docker)


### Setup
1. Run the SQL script in `mssql-init/init.sql` to create databases, tables, synonyms, and sample data.
2. Scaffold the models and contexts using EF Core (adjust connection strings as needed):
  ```sh
  # For the common database
  dotnet ef dbcontext scaffold 'Server=localhost,14331;Database=common;User Id=sa;Password=YourStrongPassw0rd;TrustServerCertificate=True;' Microsoft.EntityFrameworkCore.SqlServer --output-dir Models/Common --context-dir Data/Common --context CommonDbContext --use-database-names --no-onconfiguring --force --project POCTest/POCTest.csproj

  # For the footage database
 dotnet ef dbcontext scaffold 'Server=localhost,14332;Database=footage;User Id=sa;Password=YourStrongPassw0rd;TrustServerCertificate=True;' Microsoft.EntityFrameworkCore.SqlServer --output-dir Models/Footage --context-dir Data/Footage --context FootageDbContext --use-database-names --no-onconfiguring --force --project POCTest/POCTest.csproj
  ```
3. Update connection strings in `Program.cs` as needed for your environment.
4. Build and run the project:
  ```sh
  dotnet run --project POCTest
  ```

### Project Structure
- `POCTest/Models/Common/Club.cs` — Club entity
- `POCTest/Models/Footage/Film.cs` — Film entity
- `POCTest/Models/Footage/Film.Navigation.cs` — Navigation property for Film
- `POCTest/Data/Common/CommonDbContext.cs` — DbContext for common database
- `POCTest/Data/Footage/FootageDbContext.cs` — DbContext for footage database
- `POCTest/Data/Footage/FootageDbContext.Partial.cs` — Custom Fluent API
- `mssql-init/init.sql` — SQL setup script
- `.gitignore` — Standard .NET ignore rules

## Usage
- Query films and their clubs using navigation properties:
  ```csharp
  var filmsWithClubs = await footageContext.Films.Include(f => f.Club).ToListAsync();
  ```
- Insert new records using EF Core:
  ```csharp
  var newFilm = new Film { Title = "Test Film", ClubId = 1 };
  footageContext.Films.Add(newFilm);
  await footageContext.SaveChangesAsync();
  ```

## Notes
- The `Active` flag in `Film` is added via an `ALTER TABLE` statement in the SQL script to simulate schema evolution.
- Navigation properties are kept in partial classes to preserve custom code during scaffolding.
- SQL Server synonyms allow EF Core to treat cross-database tables as local for navigation and foreign key relationships.

## Deployments
Does it break independent deployments?

Yes, using synonyms for cross-database or cross-server access introduces a dependency between databases or instances.
If the target table, database, or server is unavailable, renamed, or its schema changes, the synonym will break and queries will fail.
This means you cannot deploy or update the databases independently without coordination, as changes in one can impact the other.
Summary:

Synonyms are convenient for cross-database access, but they tightly couple your databases and can break the principle of independent deployments.
For true independence, use APIs, ETL, or data replication instead of direct synonyms.

Summary:

Data sync jobs reduce runtime coupling but introduce operational coupling.

True independent deployments are only possible if each service/database owns its own data and does not rely on external tables, even for lookups.

For most real-world scenarios, you must balance independence, performance, and operational complexity.

If you want maximum independence, consider duplicating lookup data and updating it only via explicit releases or APIs, accepting some staleness. Otherwise, document and automate your sync/replication process as much as possible.

## License
MIT
