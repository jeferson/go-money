using GoMoney.Client;
using GoMoney.Client.Users.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Example of using the GoMoney .NET client
Console.WriteLine("GoMoney .NET Client Example");

// Method 1: Direct instantiation
const string serverUrl = "http://localhost:8080"; // Default GoMoney server URL
using var client = new GoMoneyClient(serverUrl);

try
{
    // Example: Create a user
    var createUserRequest = new CreateRequest
    {
        Username = "testuser",
        Password = "testpassword",
        Email = "test@example.com"
    };

    Console.WriteLine("Creating user...");
    var createResponse = await client.Users.CreateAsync(createUserRequest);
    
    if (createResponse.Base?.Success == true)
    {
        Console.WriteLine($"User created successfully: {createResponse.User.Username}");
    }
    else
    {
        Console.WriteLine($"Failed to create user: {createResponse.Base?.Message}");
    }

    // Example: Authenticate user
    var authRequest = new AuthenticateRequest
    {
        Username = "testuser",
        Password = "testpassword"
    };

    Console.WriteLine("Authenticating user...");
    var authResponse = await client.Users.AuthenticateAsync(authRequest);
    
    if (authResponse.Base?.Success == true)
    {
        Console.WriteLine($"User authenticated successfully. Token: {authResponse.Token[..10]}...");
    }
    else
    {
        Console.WriteLine($"Authentication failed: {authResponse.Base?.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine("Make sure the GoMoney server is running on http://localhost:8080");
}

// Method 2: Using dependency injection (for ASP.NET Core applications)
Console.WriteLine("\nExample of dependency injection setup:");
var builder = Host.CreateApplicationBuilder(args);

// Add GoMoney client to DI container
builder.Services.AddGoMoneyClient(serverUrl);

var host = builder.Build();

// Use the client from DI
using var scope = host.Services.CreateScope();
var diClient = scope.ServiceProvider.GetRequiredService<GoMoneyClient>();

Console.WriteLine("GoMoney client configured for dependency injection.");
Console.WriteLine("Ready to use in ASP.NET Core applications!");

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
