# GoMoney .NET Client

A .NET Core 8 client library for interacting with the GoMoney personal finance manager API.

## Overview

This .NET client library provides a strongly-typed interface to the GoMoney API using gRPC. It's built with .NET Core 8 and can be used in any .NET application including console apps, ASP.NET Core web applications, and more.

## Installation

### From Source

1. Clone the repository
2. Build the client library:
   ```bash
   cd dotnet
   dotnet build
   ```

3. Add a reference to your project:
   ```bash
   dotnet add reference path/to/GoMoney.Client.csproj
   ```

### NuGet Package (Future)

```bash
dotnet add package GoMoney.Client
```

## Usage

### Basic Usage

```csharp
using GoMoney.Client;
using GoMoney.Client.Users.V1;

// Create client instance
const string serverUrl = "http://localhost:8080";
using var client = new GoMoneyClient(serverUrl);

// Create a user
var createUserRequest = new CreateRequest
{
    Username = "myuser",
    Password = "mypassword", 
    Email = "user@example.com"
};

var response = await client.Users.CreateAsync(createUserRequest);
if (response.Base?.Success == true)
{
    Console.WriteLine($"User created: {response.User.Username}");
}
```

### Dependency Injection (ASP.NET Core)

```csharp
// In Program.cs or Startup.cs
builder.Services.AddGoMoneyClient("http://localhost:8080");

// In your controller or service
public class MyController : ControllerBase
{
    private readonly GoMoneyClient _client;
    
    public MyController(GoMoneyClient client)
    {
        _client = client;
    }
    
    public async Task<IActionResult> GetUser(int id)
    {
        var request = new GetRequest { Id = (uint)id };
        var response = await _client.Users.GetAsync(request);
        return Ok(response.User);
    }
}
```

## Available Services

The client currently provides access to the following GoMoney services:

- **Users**: User management and authentication
- **Accounts**: Financial account management

More services will be added as they are implemented in the API.

## Configuration

### Server URL

The client needs to know where your GoMoney server is running. By default, GoMoney runs on `http://localhost:8080`.

### Authentication

The GoMoney API uses JWT tokens for authentication. After authenticating a user, you'll receive a token that should be included in subsequent requests.

## Examples

See the [ConsoleExample](examples/ConsoleExample/) project for a complete working example.

## Building from Source

```bash
# Build the entire solution
cd dotnet
dotnet build

# Run tests (when available)
dotnet test

# Create NuGet package
dotnet pack
```

## Requirements

- .NET 8.0 or later
- GoMoney server running and accessible

## Architecture

This client uses:
- **gRPC** for communication with the GoMoney server
- **Protocol Buffers** for message serialization  
- **Microsoft.Extensions.DependencyInjection** for DI integration
- **.NET Generic Host** compatibility for configuration

## Contributing

This is a minimal implementation providing basic functionality. Contributions are welcome to:
- Add more service endpoints
- Improve error handling
- Add authentication helpers
- Add more comprehensive examples
- Add unit tests

## License

This project follows the same license as the main GoMoney project.