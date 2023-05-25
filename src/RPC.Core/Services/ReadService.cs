using Flurl.Http;
using RPC.Core.Models;
using FluentValidation;
using RPC.Core.Validation;
using Newtonsoft.Json.Linq;

namespace RPC.Core.Services;

public class ReadService
{
    private readonly string rpcConnection;

    public ReadService(string rpcConnection)
    {
        this.rpcConnection = rpcConnection;
    }

    public JToken ReadFromNetwork(RpcRequest request)
    {
        var validator = new RpcRequestValidator();
        validator.ValidateAndThrow(request);

        var response = rpcConnection.PostJsonAsync(request)
            .GetAwaiter()
            .GetResult();

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult();
    }
}
