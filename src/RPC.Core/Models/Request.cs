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
    public string? From { get; set; }
    public string To { get; set; } = null!;
    public BigInteger? Value { get; set; }
    public GasSettings? GasSettings { get; set; }
    public string FunctionName { get; set; } = null!;
    public string ABI { get; set; } = null!;
    public object[] Params { get; set; }

    public Request()
    {
        Params = Array.Empty<object>();
    }
}
