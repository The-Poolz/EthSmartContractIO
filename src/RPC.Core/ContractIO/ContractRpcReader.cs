using Flurl.Http;
using RPC.Core.Models;
using FluentValidation;
using RPC.Core.Validation;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO;

public class ContractRpcReader : RpcAction<RpcRequest>
{
    private readonly string rpcConnection;

    public ContractRpcReader(string rpcConnection)
    {
        this.rpcConnection = rpcConnection;
    }

    public override JToken ExecuteAction(RpcRequest input) =>
        ReadFromNetwork(input);

    public JToken ReadFromNetwork(RpcRequest request)
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
