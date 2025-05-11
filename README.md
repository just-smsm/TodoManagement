# README.md

```markdown
# Todo Management Application

A simple Todo management application with basic CRUD operations and status management built using ASP.NET Core MVC, Entity Framework Core, and Bootstrap.

## Features

- CRUD operations for todos
- List todos with filtering by status
- Mark todo as complete
- Input validation for required fields and dates
- Clean architecture with service layer pattern
- Bootstrap-based responsive UI

## Technical Stack

- ASP.NET Core 7/8
- Entity Framework Core
- Bootstrap 5
- SQL Server (or LocalDB)
- MVC Pattern with ViewModels

## Prerequisites

- .NET 7.0/8.0 SDK or later
- SQL Server 2019 or later (or SQL Server LocalDB)
- Visual Studio 2022 or Visual Studio Code

## Setup Instructions

### 1. Clone the Repository

```bash
git clone [repository-url]
cd TodoManagement
```

### 2. Configure Database Connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(LocalDb)\\MSSQLLocalDB;Database=TodoManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Create Database and Apply Migrations

Open Package Manager Console in Visual Studio or use .NET CLI:

```bash
# Using Package Manager Console
Add-Migration InitialCreate
Update-Database

# OR using .NET CLI
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Build the Solution

```bash
dotnet build
```

### 5. Run the Application

```bash
dotnet run
```

The application will be available at:
- `https://localhost:5001` (HTTPS)
- `http://localhost:5000` (HTTP)

## Project Structure

```
TodoManagement/
├── Controllers/            # MVC Controllers
│   └── TodoController.cs   # Main controller for Todo operations
├── Models/                 # Domain models
│   └── Todo.cs            # Todo entity
├── ViewModels/            # View Models for data binding
│   └── TodoViewModel.cs   # ViewModel for Todo operations
├── Services/              # Business logic layer
│   └── ITodoService.cs    # Service interface
│   └── TodoService.cs     # Service implementation
├── Views/                 # Razor views
│   └── Todo/             # Todo-related views
│       ├── Index.cshtml  # List view
│       ├── Create.cshtml # Create form
│       ├── Edit.cshtml   # Edit form
│       └── Delete.cshtml # Delete confirmation
├── Data/                  # DbContext and database configuration
└── wwwroot/              # Static files (CSS, JS, images)
```

## Controller Actions

| Action | HTTP Method | Route | Description |
|--------|-------------|-------|-------------|
| Index | GET | /Todo | List todos with optional status filter |
| Create | GET | /Todo/Create | Show create form |
| Create | POST | /Todo/Create | Create new todo |
| Edit | GET | /Todo/Edit/{id} | Show edit form |
| Edit | POST | /Todo/Edit/{id} | Update existing todo |
| Delete | GET | /Todo/Delete/{id} | Show delete confirmation |
| DeleteConfirmed | POST | /Todo/DeleteConfirmed/{id} | Delete todo |
| MarkComplete | POST | /Todo/MarkComplete/{id} | Mark todo as completed |

## Using the Application

1. Navigate to `/Todo` to see the list of todos
2. Click "Create New" to add a new todo item
3. Fill in the required fields (Title is mandatory)
4. Use the status filter dropdown to filter todos by status
5. Click "Edit" to modify an existing todo
6. Click "Delete" to remove a todo (with confirmation)
7. Click "Mark as Complete" to change todo status to completed

## Data Model

### Todo Entity
- **Id** (Guid): Unique identifier
- **Title** (string): Required, max 100 characters
- **Description** (string): Optional
- **Status** (enum): Pending/InProgress/Completed
- **Priority** (enum): Low/Medium/High
- **DueDate** (DateTime?): Optional
- **CreatedDate** (DateTime): Auto-set on creation
- **LastModifiedDate** (DateTime): Updated on modification

## Key Components

### TodoController
- Handles all HTTP requests for Todo operations
- Uses ITodoService for business logic
- Implements proper error handling with try-catch blocks
- Uses ViewModels for data binding

### TodoService
- Implements business logic
- Handles data access through Entity Framework
- Provides methods for CRUD operations and status management

### TodoViewModel
- Used for data transfer between views and controller
- Includes validation attributes
- Provides clean separation between domain model and view data

## Error Handling

- Controller actions include try-catch blocks
- Validation errors are displayed using ModelState
- TempData is used for displaying error messages

## Testing the Application

1. Create a new todo with all fields
2. Create a todo with only required fields
3. Edit an existing todo
4. Delete a todo
5. Mark a todo as complete
6. Filter todos by different statuses
7. Verify validation messages for invalid input
```

This updated README accurately reflects the actual implementation based on your TodoController code. It shows the MVC pattern, the actual routes and actions available, and the proper structure of your application without mentioning features that aren't implemented (like API endpoints or DataTables).
