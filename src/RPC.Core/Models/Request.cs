using RPC.Core.Types;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Models;

public class Request
{
    public ActionType ActionType { get; set; }
    public string RpcUrl { get; set; } = null!;
    public int AccountId { get; set; }
    public uint ChainId { get; set; }
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public HexBigInteger Value { get; set; } = new(0);
    public string Data { get; set; } = null!;
    public GasSettings? GasSettings { get; set; }
}
