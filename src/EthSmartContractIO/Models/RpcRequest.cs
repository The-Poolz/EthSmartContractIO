using FluentValidation;
using EthSmartContractIO.Models.Validation;
using EthSmartContractIO.ContractIO;

namespace EthSmartContractIO.Models;

/// <summary>
/// Class for managing an RPC request.
/// </summary>
public class RpcRequest
{
    public bool ActionIsRead => WriteRequest == null;
    public IContractIO CreateContractIO(IServiceProvider? serviceProvider) =>
        ActionIsRead ?
        new ContractReader(this) :
        new ContractWriter(this, serviceProvider);
    public string RpcUrl { get; }
    public string To { get; }
    public string Data { get; }
    public WriteRpcRequest? WriteRequest { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RpcRequest"/> class for a read operation or for a write operation if <see cref="WriteRpcRequest"/> not null.
    /// </summary>
    /// <param name="rpcUrl">The URL of the RPC server.</param>
    /// <param name="to">The address to send the request to.</param>
    /// <param name="data">The data to send with the request.</param>
    /// <param name="writeRequest">The write request to execute.</param>
    public RpcRequest(string rpcUrl, string to, string data = "", WriteRpcRequest? writeRequest = null)
    {
        RpcUrl = rpcUrl;
        To = to;
        Data = data;
        WriteRequest = writeRequest;

        BaseRequestValidator validator = ActionIsRead ? new ReadRequestValidator() : new WriteRequestValidator();
        validator.ValidateAndThrow(this);
    }
}
