# Employee Management System — C# / .NET

A hands-on Employee Management System built with **C#, .NET 10, and ASP.NET Core MVC**.

This project started as a console-based application and has evolved into a web application as part of my continuous learning journey in software development.

It demonstrates practical experience with object-oriented programming, LINQ, the Repository Pattern, JSON data persistence, dependency injection, unit testing, and web application development.

## Table of Contents

- [Features](#features)
- [Screenshots](#screenshots)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Data Persistence](#data-persistence)
- [Testing](#testing)
- [Learning Goals](#learning-goals)
- [Current Status](#current-status)
- [Future Improvements](#future-improvements)
- [Learning Journey](#learning-journey)

## Features

### Console Application

- Create, view, update, and delete employees
- Search employees by ID or name
- Filter employees by position
- Sort employees by name, age, and salary
- View employee dashboard and statistics
- Group employees by position
- Validate user input and handle errors
- Persist employee data using JSON
- Display success, error, and warning messages

### Web Application

- Responsive employee management dashboard
- Display employee count and position statistics
- View employee records in a table
- Add employees through a web form
- Edit employee information using a Bootstrap modal
- Delete employees with a confirmation modal
- Display success and error notifications
- Reuse business logic through `EmployeeService`
- Persist employee data using JSON

### Automated Testing

- Unit tests using xUnit
- Tests for employee service operations
- Fake repository for isolated service testing

## Screenshots

### Console Application

#### Main Menu

![Console Main Menu](Screenshots/main-menu.png)

#### Employee Dashboard

![Console Employee Dashboard](Screenshots/Dashboard.png)

#### Development Environment

![Console Development Environment](Screenshots/Code.png)

### Web Application

#### Employee Management Dashboard and CRUD

The web application provides an employee management interface for viewing, creating, editing, and deleting employee records.

![Employee Management Web Application](Screenshots/web-employees.png)

The interface includes:

- Employee ID, name, position, age, and salary
- Employee creation form
- Employee editing modal
- Delete confirmation modal
- Dashboard statistics

## Tech Stack

| Technology | Purpose |
|---|---|
| C# | Application programming language |
| .NET 10 | Application platform |
| ASP.NET Core MVC | Web application architecture |
| Razor Views | Server-rendered web pages |
| HTML, CSS, JavaScript | Web interface and interactions |
| Bootstrap | Responsive layout and modal components |
| LINQ | Searching, filtering, sorting, and grouping |
| `System.Text.Json` | JSON serialization and deserialization |
| Repository Pattern | Separation of data access and business logic |
| Dependency Injection | Managing application dependencies |
| xUnit | Unit testing |
| Git and GitHub | Version control and source code hosting |

## Project Structure

```text
CSharpDotNetLearning/
├── CSharpDotNetLearning/
│   ├── Data/
│   │   └── employees.json
│   ├── Models/
│   │   └── Employee.cs
│   ├── Repositories/
│   │   ├── IEmployeeRepository.cs
│   │   └── EmployeeRepository.cs
│   ├── Services/
│   │   └── EmployeeService.cs
│   ├── UI/
│   │   ├── EmployeeMenu.cs
│   │   └── InputHelper.cs
│   ├── CSharpDotNetLearning.csproj
│   └── Program.cs
│
├── CSharpDotNetLearning.Tests/
│   ├── EmployeeServiceTests.cs
│   ├── FakeEmployeeRepository.cs
│   └── CSharpDotNetLearning.Tests.csproj
│
├── EmployeeManagement.Web/
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── wwwroot/
│   ├── EmployeeManagement.Web.csproj
│   └── Program.cs
│
├── Screenshots/
│   ├── main-menu.png
│   ├── Dashboard.png
│   ├── Code.png
│   └── web-employees.png
│
├── .gitignore
├── CSharpDotNetLearning.slnx
└── README.md
```

## Architecture

The solution separates employee data, business logic, user interfaces, and automated tests.

### Models

The `Employee` model represents employee information, including:

- ID
- Name
- Position
- Age
- Salary

### Services

`EmployeeService` contains the application's employee-related business logic, including CRUD operations and employee search, filtering, and grouping.

### Repositories

`IEmployeeRepository` defines the repository contract, while `EmployeeRepository` handles loading and saving employee records in JSON format.

### Console Interface

The console application provides menu navigation, input handling, employee management operations, and dashboard statistics.

### Web Application

The ASP.NET Core MVC application provides a browser-based interface using controllers and Razor views.

Dependency injection connects the web application to the shared employee service and repository, allowing the console and web interfaces to reuse business logic and employee data.

### Unit Testing

The xUnit test project verifies employee service behavior. A fake repository allows service operations to be tested without relying on the actual JSON data file.

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Git
- Visual Studio or Visual Studio Code

### 1. Clone the Repository

```bash
git clone https://github.com/Chrisjohanes/csharp-employee-management-system.git
cd csharp-employee-management-system
```

### 2. Build the Solution

```bash
dotnet build CSharpDotNetLearning.slnx
```

### 3. Run the Console Application

```bash
dotnet run --project CSharpDotNetLearning/CSharpDotNetLearning.csproj
```

### 4. Run the Web Application

```bash
dotnet run --project EmployeeManagement.Web/EmployeeManagement.Web.csproj
```

Open the local URL displayed in the terminal to access the web application.

### 5. Run the Unit Tests

```bash
dotnet test CSharpDotNetLearning.slnx
```

## Data Persistence

Employee records are stored in JSON format:

```text
CSharpDotNetLearning/Data/employees.json
```

The application uses `System.Text.Json` to serialize and deserialize employee data.

Both the console and web applications are configured to use the same employee data file. Changes made through supported employee operations are saved to JSON and remain available after the application is restarted.

Because JSON file storage does not provide database-style transactions or concurrency control, avoid modifying the same file simultaneously from multiple application instances.

## Dashboard and Employee Statistics

### Console Dashboard

The console application provides statistics such as:

- Total number of employees
- Average age
- Average salary
- Highest and lowest salary
- Highest-paid employee
- Youngest and oldest employee
- Employee count by position

### Web Dashboard

The web dashboard currently displays:

- Total employee count
- Number of unique employee positions
- Employee records in a table

Employee status and joining-date statistics are potential future improvements.

## Search, Filtering, and Sorting

The console application supports:

- Searching by employee ID
- Searching by employee name
- Filtering employees by position
- Sorting by name, age, and salary

These operations use LINQ and case-insensitive text matching where applicable.

## Input Validation

The applications validate employee information to help prevent invalid data.

Validation includes required text fields, valid employee age, and non-negative salary values.

The web application also performs server-side validation for employee creation and updates.

## Learning Goals

This project helps me practice and improve my skills in:

- C# fundamentals and object-oriented programming
- Classes, objects, and collections
- LINQ and data manipulation
- Methods, loops, and conditional statements
- Exception handling
- File I/O and JSON serialization
- Input validation and error handling
- Separation of concerns
- Repository Pattern
- Dependency injection
- ASP.NET Core MVC and Razor Views
- HTML, CSS, JavaScript, and Bootstrap
- Unit testing with xUnit
- Git branching, commits, and GitHub workflows

## Current Status

**Console application and web CRUD operations implemented.**

The project has evolved from a console-based employee management system into a solution that also includes an ASP.NET Core MVC web application.

The current implementation supports employee creation, viewing, editing, and deletion, with JSON-based data persistence.

## Future Improvements

- Add search, filtering, and pagination to the web interface
- Introduce employee status and joining-date fields
- Expand automated test coverage
- Integrate SQL Server
- Introduce Entity Framework Core
- Develop an ASP.NET Core Web API
- Implement authentication and authorization
- Improve validation and error handling
- Deploy the application to a hosting platform

## Learning Journey

This project is part of my hands-on journey toward becoming a .NET developer.

I am building it incrementally, applying new concepts as I learn them, and improving the architecture as the application grows.

My goal is to evolve this project into a more complete, maintainable, and production-oriented .NET application.

## Repository

**GitHub:** [Chrisjohanes/csharp-employee-management-system](https://github.com/Chrisjohanes/csharp-employee-management-system)