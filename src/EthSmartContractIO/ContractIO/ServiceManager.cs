﻿using Nethereum.Web3;
using EthSmartContractIO.Gas;
using EthSmartContractIO.Models;
using EthSmartContractIO.Utility;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace EthSmartContractIO.ContractIO;

/// <summary>
/// Class for managing services related to Ethereum smart contracts.
/// </summary>
public class ServiceManager : Web3Base, IServiceProvider
{
    public IServiceProvider? PrimaryServiceProvider  { get; }
    public IServiceProvider BackupServiceProvider  { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceManager"/> class.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    /// <param name="serviceProvider">The service provider to use. If null, a new one will be created.</param>
    public ServiceManager(RpcRequest request, IServiceProvider? serviceProvider) :
        base(serviceProvider?.GetService<IWeb3>()
            ?? CreateWeb3(request.RpcUrl, request.WriteRequest!.AccountProvider.Account))
    {
        PrimaryServiceProvider = serviceProvider;
        BackupServiceProvider = new ServiceProviderBuilder()
            .AddWeb3(web3)
            .AddGasPricer(new GasPricer(web3))
            .AddTransactionSigner(new TransactionSigner(web3))
            .AddTransactionSender(new TransactionSender(web3))
            .Build();
    }

    /// <summary>
    /// Gets the service of the specified type.
    /// </summary>
    /// <param name="serviceType">The type of the service to get.</param>
    /// <returns>The service, or null if the service is not available.</returns>
    public object? GetService(Type serviceType)
    {
        return PrimaryServiceProvider?.GetService(serviceType)
            ?? BackupServiceProvider.GetRequiredService(serviceType);
    }
}
