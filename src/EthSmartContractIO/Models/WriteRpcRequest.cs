using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.Models;

public class WriteRpcRequest
{
    public uint ChainId { get; private set; }
    public HexBigInteger Value { get; private set; }
    public GasSettings GasSettings { get; private set; }
    public IAccountProvider AccountProvider { get; private set; }

    public WriteRpcRequest(
        uint chainId,
        HexBigInteger value,
        GasSettings gasSettings,
        IAccountProvider accountProvider
    )
    {
        ChainId = chainId;
        Value = value;
        GasSettings = gasSettings;
        AccountProvider = accountProvider;
    }
}
