using Flurl.Http;
using RPC.Core.Models;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO;

public class ContractRpcReader : IContractIO
{
    private readonly RpcRequest request;

    public ContractRpcReader(RpcRequest request)
    {
        this.request = request;
    }

    public virtual string RunContractAction()
    {
        var input = CreateActionInput();

        var response = request.RpcUrl.PostJsonAsync(input)
            .GetAwaiter()
            .GetResult();

        return ParseResponse(response);
    }

    private ReadRpcRequest CreateActionInput() =>
        new(request.To, request.Data);

    private static string ParseResponse(IFlurlResponse flurlResponse)
    {
        var response = flurlResponse.GetJsonAsync<JObject>()
            .GetAwaiter()
            .GetResult();

        return response["result"]?.ToObject<string>() ?? string.Empty;
    }
}
