using Nethereum.Hex.HexTypes;

namespace EthSmartContractIO.Gas;

public interface IGasPricer
{
    public HexBigInteger GetCurrentWeiGasPrice();
}
