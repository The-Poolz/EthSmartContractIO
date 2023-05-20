using Flurl.Http;
using Newtonsoft.Json.Linq;

namespace RPC.Core.Services;

public class ReadService
{
    private readonly string rpcConnection;

    public ReadService(string rpcConnection)
    {
        this.rpcConnection = rpcConnection;
    }

    public JToken ReadFromNetwork(JToken json)
    {
        var response = rpcConnection.PostJsonAsync(json)
            .GetAwaiter()
            .GetResult();

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult();
    }
}
