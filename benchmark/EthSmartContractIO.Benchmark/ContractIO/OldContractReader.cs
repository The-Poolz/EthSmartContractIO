using Flurl.Http;
using Newtonsoft.Json.Linq;
using EthSmartContractIO.Models;
using EthSmartContractIO.ContractIO;

namespace EthSmartContractIO.Benchmark.ContractIO;

/// <summary>
/// Class for reading data from Ethereum smart contracts.
/// </summary>
public class OldContractReader : IContractIO
{
    private readonly RpcRequest request;

    /// <summary>
    /// Initializes a new instance of the <see cref="OldContractReader"/> class.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    public OldContractReader(RpcRequest request)
    {
        this.request = request;
    }

    /// <summary>
    /// Executes a read action on the Ethereum network.
    /// </summary>
    /// <returns>The result of the action.</returns>
    /// <exception cref="FlurlHttpException">Thrown when the HTTP request fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the response does not contain the key 'result'.</exception>
    public virtual string RunContractAction()
    {
        var input = CreateActionInput();

        var response = request.RpcUrl.PostJsonAsync(input)
            .GetAwaiter()
            .GetResult();

        return ParseResponse(response);
    }

    /// <summary>
    /// Creates the input for the read action.
    /// </summary>
    /// <returns>The created <see cref="ReadRpcRequest"/>.</returns>
    private ReadRpcRequest CreateActionInput() =>
        new(request.To, request.Data);

    /// <summary>
    /// Parses the response from the Ethereum network.
    /// </summary>
    /// <param name="flurlResponse">The response to parse.</param>
    /// <returns>The parsed response.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the response does not contain the key 'result'.</exception>
    private static string ParseResponse(IFlurlResponse flurlResponse)
    {
        var response = flurlResponse.GetJsonAsync<JObject>()
            .GetAwaiter()
            .GetResult();

        return response["result"]?.ToString() ?? throw new KeyNotFoundException("Response does not contain the key 'result'.");
    }
}
