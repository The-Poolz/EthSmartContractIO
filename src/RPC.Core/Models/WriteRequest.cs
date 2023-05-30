using Nethereum.Web3;
using System.Numerics;

namespace RPC.Core.Models;

public class WriteRequest
{
    public IWeb3? Web3 { get; set; }
    public int ChainId { get; set; }
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public BigInteger? Value { get; set; }
    public GasSettings? GasSettings { get; set; }
    public string? ContractNameWithVersion { get; set; }
    public string? FunctionName { get; set; }
    public IEnumerable<object>? Params { get; set; }

    /// <summary>
    /// Added becuase user can read and write not only Poolz contract
    /// If program need to use ony Poolz contract, can be removed
    /// </summary>
    public string? ABI { get; set; }
}
