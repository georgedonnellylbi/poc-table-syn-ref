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
2. Update connection strings in `Program.cs` as needed for your environment.
3. Build and run the project:
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

## License
MIT
