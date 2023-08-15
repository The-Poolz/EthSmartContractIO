using Flurl.Http;
using Newtonsoft.Json.Linq;
using EthSmartContractIO.Models;

namespace EthSmartContractIO.ContractIO;

/// <summary>
/// Class for reading data from Ethereum smart contracts.
/// </summary>
public class ContractReader : IContractIO
{
    private const string resultName = "result";
    private readonly RpcRequest request;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractReader"/> class.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    public ContractReader(RpcRequest request) => this.request = request;

    /// <summary>
    /// Executes a read action on the Ethereum network.
    /// </summary>
    /// <returns>The result of the action.</returns>
    /// <exception cref="FlurlHttpException">Thrown when the HTTP request fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the response does not contain the key 'result'.</exception>
    public virtual string RunContractAction() =>
        PostRequest().TryGetValue(resultName, out var result)
            ? result.ToString()
            : throw new KeyNotFoundException("Response does not contain the key 'result'.");

    /// <summary>
    /// Creates the input for the read action.
    /// </summary>
    /// <returns>The created <see cref="ReadRpcRequest"/>.</returns>
    private ReadRpcRequest CreateActionInput =>
        new(request.To, request.Data);

    /// <summary>
    /// Sends the HTTP request to the Ethereum network.
    /// </summary>
    /// <returns></returns>
    private JObject PostRequest() =>
        Task.Run(() => request.RpcUrl.PostJsonAsync(CreateActionInput).ReceiveJson<JObject>()).Result;
}
