# WeCare# WeCare

WeCare is a modern ASP.NET Core MVC (.NET 10) foundation for an online blood donation and blood bank management system with a cinematic, healthcare-inspired UI.

## Prerequisites

- Visual Studio 2022 (17.10+) or VS Code with C# Dev Kit
- .NET 10 SDK
- MySQL 8.x

## Setup Guide

1. **Clone and open the solution**
2. **Optional: switch to MySQL + EF Core**
   - Add the following packages:
     - `Microsoft.EntityFrameworkCore`
     - `Pomelo.EntityFrameworkCore.MySql`
   - Configure the connection string in `appsettings.json`.
   
3. **Run the app**
   - Press `F5` in Visual Studio or run:
     ```powershell
     dotnet run
     ```

## Key Features

- Role-based registration with transactional inserts.
- Secure password hashing using PBKDF2.
- Session-based login and dashboard routing.
- Cinematic UI with glassmorphism, warm glow, and emotional imagery.

## Project Structure

- `Controllers/` → MVC controllers (`HomeController`, `RegistrationController`)
- `Models/` → Entity models and lookup tables
- `ViewModels/` → UI models for registration, login, dashboard
- `Views/` → Razor UI with cinematic styling
- `wwwroot/` → Custom CSS/JS assets

> **Note**: The workspace targets `.NET 10`. If you must use ASP.NET MVC 5 (.NET Framework 4.7.2), create a separate legacy project and adapt the same UI assets and data models.
