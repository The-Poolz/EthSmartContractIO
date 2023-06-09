namespace EthSmartContractIO.Models;

public class GasSettings
{
    public uint MaxGasLimit { get; set; }
    public uint MaxGweiGasPrice { get; set; }

    public GasSettings(uint maxGasLimit, uint maxGweiGasPrice)
    {
        MaxGasLimit = maxGasLimit;
        MaxGweiGasPrice = maxGweiGasPrice;
    }
}
