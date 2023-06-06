using RPC.Core.Types;
using FluentValidation;
using RPC.Core.Validation;

namespace RPC.Core.Models;

public class RpcRequest
{
    public ActionType ActionType { get; private set; }
    public string RpcUrl { get; private set; }
    public string To { get; private set; }
    public string Data { get; private set; }
    public WriteRpcRequest? WriteRequest { get; private set; }

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
        string to,
        string? data = null,
        WriteRpcRequest? writeRequest = null
    )
    {
        ActionType = ActionType.Write;
        RpcUrl = rpcUrl;
        To = to;
        Data = data ?? string.Empty;
        WriteRequest = writeRequest;

        new WriteRequestValidator().ValidateAndThrow(this);
    }
}
