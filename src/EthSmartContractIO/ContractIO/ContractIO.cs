using Nethereum.Web3;
using Nethereum.Contracts;
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
    public ContractIO()
        : this(null)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractIO"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use. If null, a new one will be created. Used only for write request.</param>
    public ContractIO(IServiceProvider? serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Executes a block-chain action by sending a function message to a smart contract using an Ethereum Web3 client.
    /// </summary>
    /// <typeparam name="TFunctionMessage">The type of the function message to send to the smart contract. This type must inherit from <see cref="FunctionMessage"/>.</typeparam>
    /// <typeparam name="TReturn">The return type expected from the smart contract function. This type will be deserialized from the return value of the contract function.</typeparam>
    /// <param name="web3">An instance of <see cref="IWeb3"/> used to interact with the Ethereum network.</param>
    /// <param name="to">The Ethereum address of the smart contract with which the action will be executed.</param>
    /// <param name="functionMessage">An instance of <typeparamref name="TFunctionMessage"/> containing the details of the function call to be sent to the smart contract.</param>
    /// <returns>Returns an instance of <typeparamref name="TReturn"/> that contains the result of the smart contract function execution.</returns>
    public virtual TReturn ExecuteAction<TFunctionMessage, TReturn>(IWeb3 web3, string to, TFunctionMessage functionMessage)
        where TFunctionMessage : FunctionMessage, new()
    {
        return web3.Eth.GetContractHandler(to)
            .QueryAsync<TFunctionMessage, TReturn>(functionMessage)
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// Executes a block-chain action by sending a function message to a smart contract using a specified RPC URL to connect to the Ethereum network.
    /// </summary>
    /// <typeparam name="TFunctionMessage">The type of the function message to send to the smart contract. This type must inherit from <see cref="FunctionMessage"/>.</typeparam>
    /// <typeparam name="TReturn">The return type expected from the smart contract function. This type will be deserialized from the return value of the contract function.</typeparam>
    /// <param name="rpcUrl">The URL of the Ethereum RPC endpoint to use for network interaction.</param>
    /// <param name="to">The Ethereum address of the smart contract with which the action will be executed.</param>
    /// <param name="functionMessage">An instance of <typeparamref name="TFunctionMessage"/> containing the details of the function call to be sent to the smart contract.</param>
    /// <returns>Returns an instance of <typeparamref name="TReturn"/> that contains the result of the smart contract function execution.</returns>
    public virtual TReturn ExecuteAction<TFunctionMessage, TReturn>(string rpcUrl, string to, TFunctionMessage functionMessage)
        where TFunctionMessage : FunctionMessage, new()
    {
        return ExecuteAction<TFunctionMessage, TReturn>(new Web3(rpcUrl), to, functionMessage);
    }

    /// <summary>
    /// Executes an action on the Ethereum network.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    /// <returns>The result of the action.</returns>
    public virtual string ExecuteAction(RpcRequest request) =>
        GetContractIO(request).RunContractAction();

    /// <summary>
    /// Gets the appropriate <see cref="IContractIO"/> instance for the given request.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    /// <returns>An <see cref="IContractIO"/> instance.</returns>
    private IContractIO GetContractIO(RpcRequest request) =>
        request.ActionIsRead ?
        new ContractReader(request) :
        new ContractWriter(
            request: request,
            serviceProvider: serviceProvider
        );
}
