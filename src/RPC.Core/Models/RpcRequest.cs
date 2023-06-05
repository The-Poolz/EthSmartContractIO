using RPC.Core.Types;
using Nethereum.Hex.HexTypes;
using RPC.Core.Validation;
using FluentValidation;

namespace RPC.Core.Models;

public class RpcRequest
{
    public ActionType ActionType { get; private set; }
    public string RpcUrl { get; private set; } = null!;
    public int AccountId { get; private set; }
    public uint ChainId { get; private set; }
    public string To { get; private set; } = null!;
    public HexBigInteger Value { get; private set; } = null!;
    public GasSettings GasSettings { get; private set; } = null!;
    public string Data { get; private set; } = null!;

    /// <summary>
    /// Initialize <see cref="RpcRequest"/> object for <see cref="ActionType.Read"/> operation.
    /// </summary>
    public RpcRequest(string rpcUrl, string to, string data)
    {
        ActionType = ActionType.Read;
        RpcUrl = rpcUrl;
        To = to;
        Data = data;

        new ReadRequestValidator().ValidateAndThrow(this);
    }

    /// <summary>
    /// Initialize <see cref="RpcRequest"/> object for <see cref="ActionType.Write"/> operation.
    /// </summary>
    public RpcRequest(
        string rpcUrl,
        int accountId,
        uint chainId,
        string to,
        HexBigInteger value,
        GasSettings gasSettings,
        string? data = null
    )
    {
        ActionType = ActionType.Write;
        RpcUrl = rpcUrl;
        AccountId = accountId;
        ChainId = chainId;
        To = to;
        Value = value;
        GasSettings = gasSettings;
        Data = data ?? string.Empty;

        new WriteRequestValidator().ValidateAndThrow(this);
    }
}
