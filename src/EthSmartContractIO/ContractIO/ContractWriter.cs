using EthSmartContractIO.Gas;
using EthSmartContractIO.Models;
using EthSmartContractIO.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace EthSmartContractIO.ContractIO;

/// <summary>
/// Class for writing data to Ethereum smart contracts.
/// </summary>
public class ContractWriter : IContractIO
{
    private readonly RpcRequest request;
    private IServiceProvider serviceProvider;
    private IGasPricer GasPricer =>
        serviceProvider.GetRequiredService<IGasPricer>();
    private ITransactionSigner TransactionSigner =>
        serviceProvider.GetRequiredService<ITransactionSigner>();
    private ITransactionSender TransactionSender =>
        serviceProvider.GetRequiredService<ITransactionSender>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractWriter"/> class.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    /// <param name="serviceProvider">The service provider to use. If null, a new one will be created.</param>
    public ContractWriter(RpcRequest request) 
    {
        serviceProvider = new ServiceManager(request, null);
        this.request = request;
    }

    public ContractWriter SetServiceProvider(IServiceProvider? serviceProvider)
    {
        if (serviceProvider != null)
            this.serviceProvider = new ServiceManager(request, serviceProvider);
        return this;
    }

    /// <summary>
    /// Executes a write action on the Ethereum network.
    /// </summary>
    /// <returns>The result of the action.</returns>
    public virtual string RunContractAction() =>
        TransactionSender.SendTransaction(
            TransactionSigner.SignTransaction(new AssembledTransaction(request, GasPricer)));
}
