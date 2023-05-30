using Nethereum.Web3;
using RPC.Core.Types;
using System.Numerics;

namespace RPC.Core.Models;

public class Request
{
    public ActionType ActionType { get; set; }
    public string? RpcUrl { get; set; }
    public IWeb3? Web3 { get; set; }
    public uint ChainId { get; set; }
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public BigInteger? Value { get; set; }
    public string? Data { get; set; }
    public GasSettings GasSettings { get; set; }

    public Request()
    {
        GasSettings = new();
    }
}
