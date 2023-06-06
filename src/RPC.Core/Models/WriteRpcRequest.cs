using RPC.Core.Providers;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Models;

public class WriteRpcRequest
{
    public int AccountId { get; private set; }
    public uint ChainId { get; private set; }
    public HexBigInteger Value { get; private set; }
    public GasSettings GasSettings { get; private set; }
    public IMnemonicProvider MnemonicProvider { get; private set; }

    public WriteRpcRequest(
        int accountId,
        uint chainId,
        HexBigInteger value,
        GasSettings gasSettings,
        IMnemonicProvider mnemonicProvider
    )
    {
        AccountId = accountId;
        ChainId = chainId;
        Value = value;
        GasSettings = gasSettings;
        MnemonicProvider = mnemonicProvider;
    }
}
