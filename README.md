# SalesApp

A simple Sales app with an API and a Razor Pages Web UI for managing orders.

## Tech
- .NET 10
- ASP.NET Core Web API
- Razor Pages
- In‑Memory EF Core database

## Prerequisites
- .NET SDK 10.x installed

## Quick Start
1. Restore packages:
   - `dotnet restore`
2. Run the API:
   - `dotnet run --project src/Api`
3. Run the WebUI:
   - `dotnet run --project src/WebUI`
4. Open the Web UI:
   - `http://localhost:5293/Orders`

## Notes
- The API uses an in‑memory database, so data resets when the API restarts.
- If you change ports, update the API base URL in:
  - `src/WebUI/Pages/Orders/Index.cshtml.cs`
  - `src/WebUI/wwwroot/js/orderForm.js`
  - `src/WebUI/Pages/Orders/_OrderListPartial.cshtml`

## Project Structure
- `src/Api` — Web API
- `src/WebUI` — Razor Pages UI
- `src/Application` — Application layer (MediatR handlers, discounts)
- `src/Domain` — Entities
- `src/Infrastructure` — EF Core, repositories, unit of work
