using Nethereum.Web3;
using EthSmartContractIO.Gas;
using Nethereum.Web3.Accounts;
using EthSmartContractIO.Models;
using EthSmartContractIO.Utility;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Transaction;
using EthSmartContractIO.Providers.Account;
using Microsoft.Extensions.DependencyInjection;

namespace EthSmartContractIO.ContractIO;

public class ServiceManager : IServiceProvider
{
    public Account Account { get; }
    public IServiceProvider? PrimaryServiceProvider { get; }
    public IServiceProvider BackupServiceProvider { get; }
    public ServiceManager(RpcRequest request, IServiceProvider? serviceProvider)
    {
        PrimaryServiceProvider = serviceProvider;
        Account = serviceProvider?.GetService<IAccountProvider>()?.GetAccount(request.WriteRequest!.AccountParams) ??
            new PrivateKeyAccountProvider(request.WriteRequest!.AccountParams).GetAccount();

        var web3 = serviceProvider?.GetService<IWeb3>()
            ?? Web3Base.CreateWeb3(request.RpcUrl, Account);

        BackupServiceProvider = new ServiceProviderBuilder()
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
