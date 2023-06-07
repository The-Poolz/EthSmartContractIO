using Nethereum.Hex.HexTypes;

namespace RPC.Core.Gas;

public interface IGasPricer
{
    public HexBigInteger GetCurrentWeiGasPrice();
}
