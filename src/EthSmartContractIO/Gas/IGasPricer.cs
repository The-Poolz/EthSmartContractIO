using Nethereum.Hex.HexTypes;

namespace EthSmartContractIO.Gas;

/// <summary>
/// Interface for pricing gas usage.
/// </summary>
public interface IGasPricer
{
    /// <summary>
    /// Gets the current gas price in wei.
    /// </summary>
    /// <returns>The current gas price in wei.</returns>
    public HexBigInteger GetCurrentWeiGasPrice();
}
