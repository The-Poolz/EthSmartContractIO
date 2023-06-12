using Nethereum.Hex.HexTypes;

namespace EthSmartContractIO.Models;

public class WriteRpcRequest
{
    public HexBigInteger Value { get; private set; }
    public GasSettings GasSettings { get; private set; }
    public object[] AccountParams { get; private set; }

    public WriteRpcRequest(
        HexBigInteger value,
        GasSettings gasSettings,
        params object[] accountParams
    )
    {
        Value = value;
        GasSettings = gasSettings;
        AccountParams = accountParams;
    }
}
