using Flurl.Http;
using RPC.Core.Models;
using Newtonsoft.Json.Linq;

namespace RPC.Core.Services;

public class ReadService
{
    private readonly string rpcConnection;

    public ReadService(string rpcConnection)
    {
        this.rpcConnection = rpcConnection;
    }

    public JToken ReadFromNetwork(JsonRpcRequest request)
    {
        var response = rpcConnection.PostJsonAsync(request)
            .GetAwaiter()
            .GetResult();

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult();
    }
}
