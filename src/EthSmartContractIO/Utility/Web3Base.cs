using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace EthSmartContractIO.Utility;

/// <summary>
/// Base class for classes that use Web3.
/// </summary>
public abstract class Web3Base
{
    protected readonly IWeb3 web3;

    /// <summary>
    /// Initializes a new instance of the <see cref="Web3Base"/> class.
    /// </summary>
    /// <param name="web3">The <see cref="Web3"/> instance to use.</param>
    protected Web3Base(IWeb3 web3)
    {
        this.web3 = web3;
    }

    /// <summary>
    /// Creates a new <see cref="Web3"/> instance.<br/>
    /// The account is used to sign transactions.<br/>
    /// The RPC connection is used to connect to the blockchain.
    /// </summary>
    /// <param name="rpcConnection">The RPC connection to use.</param>
    /// <param name="account">The account to use. </param>
    /// <returns>The created Web3 instance.</returns>
    public static IWeb3 CreateWeb3(string rpcConnection, Account account)
    {
        var client = new RpcClient(new Uri(rpcConnection));
        return new Web3(account, client);
    }

    public static IWeb3 CreateWeb3(Models.RpcRequest request) =>
        CreateWeb3(request.RpcUrl, request.WriteRequest!.AccountProvider.Account);
}
