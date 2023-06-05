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

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult()
            .ToString();
    }

    private ReadRpcRequest CreateActionInput() =>
        new(request.To, request.Data);
}
