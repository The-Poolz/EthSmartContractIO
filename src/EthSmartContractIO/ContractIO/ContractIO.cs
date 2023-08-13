using EthSmartContractIO.Models;

namespace EthSmartContractIO.ContractIO;

/// <summary>
/// Class for interacting with Ethereum smart contracts.
/// </summary>
public class ContractIO
{
    private readonly IServiceProvider? serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractIO"/> class.
    /// </summary>
    public ContractIO() : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractIO"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use. If null, a new one will be created. Used only for write request.</param>
    public ContractIO(IServiceProvider? serviceProvider) =>
        this.serviceProvider = serviceProvider;

    /// <summary>
    /// Executes an action on the Ethereum network.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    /// <returns>The result of the action.</returns>
    public virtual string ExecuteAction(RpcRequest request) =>
         ((IContractIO)Activator.CreateInstance(request.CreateContractIO, request, serviceProvider))
        .RunContractAction();
}
