namespace RPC.Core.Models;

public class ReadRequest
{
    /// <summary>
    /// On RpcUrlManager be done, can remove this prop and use only ChainId for getting RpcUrl
    /// </summary>
    public string? RpcUrl { get; set; }
    public string To { get; set; } = null!;
    public string? ContractNameWithVersion { get; set; }
    public string? FunctionName { get; set; }
    public IEnumerable<object>? Params { get; set; }

    /// <summary>
    /// Added becuase user can read and write not only Poolz contract
    /// If program need to use ony Poolz contract, can be removed
    /// </summary>
    public string? ABI { get; set; }
}
