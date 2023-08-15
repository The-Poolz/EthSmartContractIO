using Flurl.Http;
using Newtonsoft.Json.Linq;
using EthSmartContractIO.Models;
using EthSmartContractIO.ContractIO;

namespace EthSmartContractIO.Benchmark.ContractIO;

/// <summary>
/// Class for reading data from Ethereum smart contracts.
/// </summary>
public class NewContractReader : IContractIO
{
    private const string resultName = "result";
    private readonly RpcRequest request;

    /// <summary>
    /// Initializes a new instance of the <see cref="NewContractReader"/> class.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    public NewContractReader(RpcRequest request) => this.request = request;

    /// <summary>
    /// Executes a read action on the Ethereum network.
    /// </summary>
    /// <returns>The result of the action.</returns>
    /// <exception cref="FlurlHttpException">Thrown when the HTTP request fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the response does not contain the key 'result'.</exception>
    public virtual string RunContractAction() =>
        (PostRequest[resultName] ??
        throw new KeyNotFoundException("Response does not contain the key 'result'.")).ToString();


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
    private JObject PostRequest =>  request.RpcUrl.PostJsonAsync(CreateActionInput)
        .ReceiveJson<JObject>().GetAwaiter().GetResult();
}
