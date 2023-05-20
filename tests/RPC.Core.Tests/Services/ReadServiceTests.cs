using Xunit;
using RPC.Core.Services;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;

namespace RPC.Core.Tests.Services;

public class ReadServiceTests
{
    [Fact]
    internal void ReadFromNetwork_ExpectedJson()
    {
        var rpcConnection = "http://localhost:8545/";
        var request = new JObject()
        {
            { "jsonrpc", "2.0" },
            { "method", "eth_blockNumber" },
            { "params", new JArray() },
            { "id", 0 }
        };
        var response = new JObject()
        {
            { "jsonrpc", "2.0" },
            { "method", "eth_blockNumber" },
            { "result", "0x1c93124" },
            { "id", 0 }
        };
        var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(rpcConnection)
            .RespondWithJson(response);

        var result = new ReadService(rpcConnection).ReadFromNetwork(request);

        Assert.NotNull(result);
        Assert.Equal(response, result);
    }
}
