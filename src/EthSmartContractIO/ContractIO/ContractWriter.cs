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
    private ServiceManager serviceManager;
    private IGasPricer GasPricer =>
        serviceManager.GetRequiredService<IGasPricer>();
    private ITransactionSigner TransactionSigner =>
        serviceManager.GetRequiredService<ITransactionSigner>();
    private ITransactionSender TransactionSender =>
        serviceManager.GetRequiredService<ITransactionSender>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractWriter"/> class.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    public ContractWriter(RpcRequest request) 
    {
        this.request = request;
        serviceManager = new ServiceManager(request);
    }

    public ContractWriter SetServiceProvider(IServiceProvider? serviceProvider)
    {
        serviceManager = new ServiceManager(request, serviceProvider);
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
