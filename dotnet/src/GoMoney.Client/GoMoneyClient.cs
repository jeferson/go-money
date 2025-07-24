using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using GoMoney.Client.Users.V1;
using GoMoney.Client.Accounts.V1;

namespace GoMoney.Client;

/// <summary>
/// Main client for interacting with the GoMoney API
/// </summary>
public class GoMoneyClient : IDisposable
{
    private readonly GrpcChannel _channel;
    private readonly UsersService.UsersServiceClient _usersClient;
    private readonly AccountsService.AccountsServiceClient _accountsClient;

    public GoMoneyClient(string baseAddress)
    {
        _channel = GrpcChannel.ForAddress(baseAddress);
        _usersClient = new UsersService.UsersServiceClient(_channel);
        _accountsClient = new AccountsService.AccountsServiceClient(_channel);
    }

    public GoMoneyClient(GrpcChannel channel)
    {
        _channel = channel;
        _usersClient = new UsersService.UsersServiceClient(_channel);
        _accountsClient = new AccountsService.AccountsServiceClient(_channel);
    }

    /// <summary>
    /// Client for user operations
    /// </summary>
    public UsersService.UsersServiceClient Users => _usersClient;

    /// <summary>
    /// Client for account operations
    /// </summary>
    public AccountsService.AccountsServiceClient Accounts => _accountsClient;

    public void Dispose()
    {
        _channel?.Dispose();
    }
}

/// <summary>
/// Extensions for dependency injection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds GoMoney client to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="baseAddress">The base address of the GoMoney API</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddGoMoneyClient(this IServiceCollection services, string baseAddress)
    {
        services.AddGrpcClient<UsersService.UsersServiceClient>(options =>
        {
            options.Address = new Uri(baseAddress);
        });

        services.AddGrpcClient<AccountsService.AccountsServiceClient>(options =>
        {
            options.Address = new Uri(baseAddress);
        });

        services.AddScoped<GoMoneyClient>(provider =>
        {
            var channel = GrpcChannel.ForAddress(baseAddress);
            return new GoMoneyClient(channel);
        });

        return services;
    }
}