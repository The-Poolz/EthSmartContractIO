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

    public JToken ReadFromNetwork(string to, string data)
    {
        var response = rpcConnection.PostJsonAsync(new JsonRpcRequest(to, data))
            .GetAwaiter()
            .GetResult();

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult();
    }
}
