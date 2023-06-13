using Nethereum.Web3;
using EthSmartContractIO.Gas;
using EthSmartContractIO.Models;
using EthSmartContractIO.Utility;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace EthSmartContractIO.ContractIO;

public class ServiceManager : Web3Base, IServiceProvider
{
    public IServiceProvider? PrimaryServiceProvider  { get; }
    public IServiceProvider BackupServiceProvider  { get; }
    public ServiceManager(RpcRequest request, IServiceProvider? serviceProvider) :
        base(serviceProvider?.GetService<IWeb3>()
            ?? CreateWeb3(request.RpcUrl, request.WriteRequest!.AccountProvider.Account))
    {
        PrimaryServiceProvider  = serviceProvider;
        BackupServiceProvider  = new ServiceProviderBuilder()
            .AddWeb3(web3)
            .AddGasPricer(new GasPricer(web3))
            .AddTransactionSigner(new TransactionSigner(web3))
            .AddTransactionSender(new TransactionSender(web3))
            .Build();
    }

    public object? GetService(Type serviceType)
    {
        return PrimaryServiceProvider?.GetService(serviceType)
            ?? BackupServiceProvider.GetRequiredService(serviceType);
    }
}
