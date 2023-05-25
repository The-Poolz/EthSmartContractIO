using Nethereum.Web3;

namespace RPC.Core.Gas;

public abstract class GasBase
{
    protected readonly IWeb3 web3;

    protected GasBase(IWeb3 web3)
    {
        this.web3 = web3;
    }
}
