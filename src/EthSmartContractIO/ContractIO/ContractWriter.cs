using EthSmartContractIO.Gas;
using EthSmartContractIO.Models;
using EthSmartContractIO.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace EthSmartContractIO.ContractIO;

public class ContractWriter : IContractIO
{
    private readonly RpcRequest request;
    private readonly IServiceProvider serviceProvider;
    private IGasPricer GasPricer =>
        serviceProvider.GetRequiredService<IGasPricer>();
    private ITransactionSigner TransactionSigner =>
        serviceProvider.GetRequiredService<ITransactionSigner>();
    private ITransactionSender TransactionSender =>
        serviceProvider.GetRequiredService<ITransactionSender>();

    public ContractWriter(RpcRequest request, IServiceProvider? serviceProvider = null) 
    {
        this.serviceProvider = new ServiceManager(request, serviceProvider);
        this.request = request;
    }

    public virtual string RunContractAction() =>
        TransactionSender.SendTransaction(
            TransactionSigner.SignTransaction(new AssembledTransaction(request, GasPricer)));
}
