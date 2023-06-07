using Nethereum.Web3;
using RPC.Core.Utility;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Gas;

public class GasPricer : Web3Base, IGasPricer
{
    public GasPricer(IWeb3 web3) : base(web3) { }

    public HexBigInteger GetCurrentWeiGasPrice() =>
        web3.Eth.GasPrice.SendRequestAsync()
            .GetAwaiter()
            .GetResult();
}
