using Flurl.Http;
using RPC.Core.Models;
using FluentValidation;
using RPC.Core.Validation;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO;

public class ContractRpcReader : IRpcAction
{
    private readonly string rpcConnection;

    public ContractRpcReader(string rpcConnection)
    {
        this.rpcConnection = rpcConnection;
    }

    public string ExecuteAction(Request request)
    {
        var input = CreateActionInput(request);
        return ReadFromNetwork(input).ToString();
    }

    private RpcRequest CreateActionInput(Request request) =>
        new(request.To, request.Data);

    private JToken ReadFromNetwork(RpcRequest request)
    {
        new RpcRequestValidator().ValidateAndThrow(request);

        var response = rpcConnection.PostJsonAsync(request)
            .GetAwaiter()
            .GetResult();

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult();
    }
}
