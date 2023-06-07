using RPC.Core.Gas;
using Nethereum.Web3;
using RPC.Core.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace RPC.Core.Providers;

public class ServiceProviderBuilder
{
    private readonly IServiceCollection services;

    public ServiceProviderBuilder()
    {
        services = new ServiceCollection();
    }

    public ServiceProvider Build()
    {
        return services.BuildServiceProvider();
    }

    public ServiceProviderBuilder AddWeb3()
    {
        services.AddSingleton<IWeb3>();
        return this;
    }

    public ServiceProviderBuilder AddGasEstimator()
    {
        services.AddSingleton<IGasEstimator>();
        return this;
    }

    public ServiceProviderBuilder AddGasPricer()
    {
        services.AddSingleton<IGasPricer>();
        return this;
    }

    public ServiceProviderBuilder AddTransactionSigner()
    {
        services.AddSingleton<ITransactionSigner>();
        return this;
    }

    public ServiceProviderBuilder AddTransactionSender()
    {
        services.AddSingleton<ITransactionSender>();
        return this;
    }
}
