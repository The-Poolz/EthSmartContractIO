using RPC.Core.Providers;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Models;

public class WriteRpcRequest
{
    public int AccountId { get; private set; }
    public uint ChainId { get; private set; }
    public HexBigInteger Value { get; private set; } = null!;
    public GasSettings GasSettings { get; private set; } = null!;
    public IMnemonicProvider MnemonicProvider { get; private set; } = null!;
}
