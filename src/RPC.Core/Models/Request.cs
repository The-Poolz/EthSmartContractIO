using Nethereum.Web3;
using RPC.Core.Types;
using System.Numerics;

namespace RPC.Core.Models;

public class Request
{
    public ActionType ActionType { get; set; }
    public IWeb3? Web3 { get; set; }
    // On RpcUrlManager be done, can remove this prop and use only ChainId for getting RpcUrl
    public string? RpcUrl { get; set; }
    public int ChainId { get; set; }
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public BigInteger? Value { get; set; }
    public GasSettings? GasSettings { get; set; }
    public string? ContractNameWithVersion { get; set; }
    public string? FunctionName { get; set; }
    public IEnumerable<object>? Params { get; set; }
}
