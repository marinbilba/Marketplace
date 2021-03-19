# Marketplace

Solution Structure:
Frontend: Blazor <-> REST <-> Backend: .NET Core <-> Database: PostgreSQL


1. In order to run the Backend it is required to change the DatabaseConnectionString(MarketplaceAPI/Database/MarketplaceContex) in the method
OnConfiguring() following the pattern: Host={your host};Database={your database name};Username={your username};Password={your password}.
2. Apply the database migrations(configured for PostgreSQL) with following commands: 

dotnet ef migrations add InitialCrate
dotnet ef database update

3. Now everything is set up. Run the MarketplaceAPI and MarketplaceAPP projects. 

Note: There is only one user loaded in the database: username:test || password:123


