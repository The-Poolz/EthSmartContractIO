using Flurl.Http;
using RPC.Core.Models;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO;

public class ContractRpcReader : IContractIO
{
    private readonly Request request;

    public ContractRpcReader(Request request)
    {
        this.request = request;
    }

    public virtual string RunContractAction()
    {
        var input = CreateActionInput();

        var response = request.RpcUrl.PostJsonAsync(input)
            .GetAwaiter()
            .GetResult();

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult()
            .ToString();
    }

    private RpcRequest CreateActionInput() =>
        new(request.To, request.Data);
}
