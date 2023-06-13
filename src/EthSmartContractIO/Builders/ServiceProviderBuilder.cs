using Nethereum.Web3;
using EthSmartContractIO.Gas;
using EthSmartContractIO.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace EthSmartContractIO.Builders;

/// <summary>
/// Builder class for creating and configuring a service provider.
/// </summary>
public class ServiceProviderBuilder
{
    private readonly IServiceCollection services;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceProviderBuilder"/> class.
    /// </summary>
    public ServiceProviderBuilder()
    {
        services = new ServiceCollection();
    }

    /// <summary>
    /// Builds the service provider with the configured services.
    /// </summary>
    /// <returns>The built service provider.</returns>
    public ServiceProvider Build()
    {
        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Adds a singleton instance of <see cref="IWeb3"/> to the service collection.
    /// </summary>
    /// <param name="web3">The <see cref="IWeb3"/> instance to add.</param>
    /// <returns>The <see cref="ServiceProviderBuilder"/> instance.</returns>
    public ServiceProviderBuilder AddWeb3(IWeb3 web3)
    {
        services.AddSingleton(web3);
        return this;
    }

    /// <summary>
    /// Adds a singleton instance of <see cref="IGasPricer"/> to the service collection.
    /// </summary>
    /// <param name="gasPricer">The <see cref="IGasPricer"/> instance to add.</param>
    /// <returns>The <see cref="ServiceProviderBuilder"/> instance.</returns>
    public ServiceProviderBuilder AddGasPricer(IGasPricer gasPricer)
    {
        services.AddSingleton(gasPricer);
        return this;
    }

    /// <summary>
    /// Adds a singleton instance of <see cref="ITransactionSigner"/> to the service collection.
    /// </summary>
    /// <param name="transactionSigner">The <see cref="ITransactionSigner"/> instance to add.</param>
    /// <returns>The <see cref="ServiceProviderBuilder"/> instance.</returns>
    public ServiceProviderBuilder AddTransactionSigner(ITransactionSigner transactionSigner)
    {
        services.AddSingleton(transactionSigner);
        return this;
    }

    /// <summary>
    /// Adds a singleton instance of <see cref="ITransactionSender"/> to the service collection.
    /// </summary>
    /// <param name="transactionSender">The <see cref="ITransactionSender"/> instance to add.</param>
    /// <returns>The <see cref="ServiceProviderBuilder"/> instance.</returns>
    public ServiceProviderBuilder AddTransactionSender(ITransactionSender transactionSender)
    {
        services.AddSingleton(transactionSender);
        return this;
    }
}
