using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Utility;

namespace EthSmartContractIO.Gas;

/// <summary>
/// Class for pricing gas usage.
/// </summary>
public class GasPricer : Web3Base, IGasPricer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GasPricer"/> class.
    /// </summary>
    /// <param name="web3">The <see cref="Web3"/> instance to use.</param>
    public GasPricer(IWeb3 web3) : base(web3) { }

    /// <summary>
    /// Gets the current gas price in wei.
    /// </summary>
    /// <returns>The current gas price in wei.</returns>
    public HexBigInteger GetCurrentWeiGasPrice() =>
        web3.Eth.GasPrice.SendRequestAsync()
            .GetAwaiter()
            .GetResult();
}
