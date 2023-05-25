using Nethereum.Web3;

namespace RPC.Core.Transaction;

public abstract class TransactionBase
{
    protected readonly IWeb3 web3;

    protected TransactionBase(IWeb3 web3)
    {
        this.web3 = web3;
    }
}
