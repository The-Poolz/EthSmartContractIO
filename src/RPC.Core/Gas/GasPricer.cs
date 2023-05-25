using Nethereum.Web3;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Gas;

public class GasPricer : GasBase
{
    public GasPricer(IWeb3 web3) : base(web3) { }

    public HexBigInteger GetCurrentWeiGasPrice() =>
        web3.Eth.GasPrice.SendRequestAsync()
            .GetAwaiter()
            .GetResult();
}
