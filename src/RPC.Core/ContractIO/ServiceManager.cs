using Microsoft.Extensions.DependencyInjection;
using Nethereum.Web3;
using RPC.Core.Builders;
using RPC.Core.Gas;
using RPC.Core.Models;
using RPC.Core.Transaction;
using RPC.Core.Utility;

namespace RPC.Core.ContractIO
{
    public class ServiceManager : Web3Base, IServiceProvider
    {
        public IServiceProvider? PrimaryServiceProvider  { get; }
        public IServiceProvider BackupServiceProvider  { get; }
        public ServiceManager(RpcRequest request, IServiceProvider? ServiceProvider) :
            base(ServiceProvider?.GetService<IWeb3>() ??
                CreateWeb3(request.RpcUrl, request.WriteRequest!.AccountProvider.Account))
        {
            PrimaryServiceProvider  = ServiceProvider;
            BackupServiceProvider  = new ServiceProviderBuilder()
                .AddWeb3(web3)
                .AddGasPricer(new GasPricer(web3))
                .AddTransactionSigner(new TransactionSigner(web3))
                .AddTransactionSender(new TransactionSender(web3))
                .Build();
        }

        public object? GetService(Type serviceType)
        {
            return PrimaryServiceProvider ?.GetService(serviceType)
                ?? BackupServiceProvider .GetRequiredService(serviceType);
        }
    }
}
