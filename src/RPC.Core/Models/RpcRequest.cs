using FluentValidation;
using RPC.Core.Validation;

namespace RPC.Core.Models;

public class RpcRequest
{
    public bool ActionIsRead => WriteRequest == null;
    public string RpcUrl { get; private set; }
    public string To { get; private set; }
    public string Data { get; private set; }
    public WriteRpcRequest? WriteRequest { get; private set; }

    /// <summary>
    /// Initialize <see cref="RpcRequest"/> object for Read operation.
    /// </summary>
    public RpcRequest(string rpcUrl, string to, string data)
    {
        RpcUrl = rpcUrl;
        To = to;
        Data = data;

        new ReadRequestValidator().ValidateAndThrow(this);
    }

    /// <summary>
    /// Initialize <see cref="RpcRequest"/> object for Write operation.
    /// </summary>
    public RpcRequest(
        string rpcUrl,
        string to,
        WriteRpcRequest writeRequest,
        string? data = null
    )
    {
        RpcUrl = rpcUrl;
        To = to;
        Data = data ?? string.Empty;
        WriteRequest = writeRequest;

        new WriteRequestValidator().ValidateAndThrow(this);
    }
}
