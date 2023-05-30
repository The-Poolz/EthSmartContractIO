using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Utility;

public abstract class Web3Base
{
    protected readonly IWeb3 web3;

    protected Web3Base(IWeb3 web3)
    {
        this.web3 = web3;
    }

    public static IWeb3 CreateWeb3(string rpcConnection, Account account)
    {
        var client = new RpcClient(new Uri(rpcConnection));
        return new Web3(account, client);
    }
}
