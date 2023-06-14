using FluentValidation;
using EthSmartContractIO.Models.Validation;

namespace EthSmartContractIO.Models;

/// <summary>
/// Class for managing an RPC request.
/// </summary>
public class RpcRequest
{
    public bool ActionIsRead => WriteRequest == null;
    public string RpcUrl { get; private set; }
    public string To { get; private set; }
    public string Data { get; private set; }
    public WriteRpcRequest? WriteRequest { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RpcRequest"/> class for a read operation.
    /// </summary>
    /// <param name="rpcUrl">The URL of the RPC server.</param>
    /// <param name="to">The address to send the request to.</param>
    /// <param name="data">The data to send with the request.</param>
    public RpcRequest(string rpcUrl, string to, string data)
    {
        RpcUrl = rpcUrl;
        To = to;
        Data = data;

        new ReadRequestValidator().ValidateAndThrow(this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RpcRequest"/> class for a write operation.
    /// </summary>
    /// <param name="rpcUrl">The URL of the RPC server.</param>
    /// <param name="to">The address to send the request to.</param>
    /// <param name="writeRequest">The write request to execute.</param>
    /// <param name="data">The data to send with the request.</param>
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
