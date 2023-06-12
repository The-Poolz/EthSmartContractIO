using Nethereum.Web3;
using EthSmartContractIO.Gas;
using EthSmartContractIO.Models;
using EthSmartContractIO.Utility;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Transaction;
using Microsoft.Extensions.DependencyInjection;
using EthSmartContractIO.AccountProvider;
using EthSmartContractIO.Providers;
using Nethereum.Web3.Accounts;

namespace EthSmartContractIO.ContractIO;

public class ServiceManager : IServiceProvider
{
    public IWeb3 Web3 { get; }
    public Account Account { get; }
    public IServiceProvider? PrimaryServiceProvider { get; }
    public IServiceProvider BackupServiceProvider { get; }
    public ServiceManager(RpcRequest request, IServiceProvider? ServiceProvider)
    {
        PrimaryServiceProvider = ServiceProvider;
        Account = ServiceProvider?.GetService<IAccountProvider>()?.Account ??
            new PrivateKeyAccountProvider(request.WriteRequest!.AccountParams).Account; 

        Web3 = ServiceProvider?.GetService<IWeb3>()
            ?? Web3Base.CreateWeb3(request.RpcUrl, Account);

        BackupServiceProvider = new ServiceProviderBuilder()
            .AddGasPricer(new GasPricer(Web3))
            .AddTransactionSigner(new TransactionSigner(Web3))
            .AddTransactionSender(new TransactionSender(Web3))
        .Build();
    }

    public object? GetService(Type serviceType)
    {
        return PrimaryServiceProvider?.GetService(serviceType)
            ?? BackupServiceProvider.GetRequiredService(serviceType);
    }
}
