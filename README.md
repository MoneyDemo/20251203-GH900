# 20251203-GH900

## TodoDemo - ASP.NET Core MVC TODO Application

This repository contains a simple TODO management application built with ASP.NET Core 10.0 MVC.

### Features
- Create, Read, Update, Delete (CRUD) operations for TODO items
- Mark items as complete/incomplete
- In-memory data storage

### GitHub Actions CI/CD

#### Unit Test Workflow
The repository includes a GitHub Actions workflow that automatically runs unit tests on every push and pull request.

**Workflow File:** `.github/workflows/unit-test.yml`

**Triggers:**
- Push to `main` or `develop` branches
- Pull requests targeting `main` or `develop` branches

**Workflow Steps:**
1. **Checkout code** - Retrieves the repository code
2. **Setup .NET** - Installs .NET 10.0 SDK
3. **Restore dependencies** - Downloads required NuGet packages
4. **Build** - Compiles the test project in Release configuration
5. **Run tests** - Executes all unit tests with detailed logging
6. **Upload test results** - Saves test results as artifacts (TRX format)

**Test Coverage:**
- TodoService CRUD operations
- Edge cases and error handling
- Data validation

### Running Tests Locally

```bash
# Navigate to the test project directory
cd TodoDemo.Tests

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity detailed

# Generate test results report
dotnet test --logger "trx;LogFileName=test-results.trx"
```

### Building the Application

```bash
# Restore dependencies
dotnet restore

# Build the application
dotnet build

# Run the application
cd TodoDemo
dotnet run
```

### Project Structure

```
.
├── .github/
│   └── workflows/
│       └── unit-test.yml       # GitHub Actions workflow
├── TodoDemo/                   # Main application
│   ├── Controllers/            # MVC Controllers
│   ├── Models/                 # Data models
│   ├── Services/               # Business logic
│   └── Views/                  # Razor views
└── TodoDemo.Tests/             # Unit tests
    └── TodoServiceTests.cs     # Tests for TodoService
```