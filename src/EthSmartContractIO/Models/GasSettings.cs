namespace EthSmartContractIO.Models;

/// <summary>
/// Class for managing gas settings.
/// </summary>
public class GasSettings
{
    public uint MaxGasLimit { get; set; }
    public uint MaxGweiGasPrice { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GasSettings"/> class.
    /// </summary>
    /// <param name="maxGasLimit">The maximum gas limit.</param>
    /// <param name="maxGweiGasPrice">The maximum gas price in Gwei.</param>
    public GasSettings(uint maxGasLimit, uint maxGweiGasPrice)
    {
        MaxGasLimit = maxGasLimit;
        MaxGweiGasPrice = maxGweiGasPrice;
    }
}
