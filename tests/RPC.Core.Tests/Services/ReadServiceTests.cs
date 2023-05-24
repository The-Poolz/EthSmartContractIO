using Xunit;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;

namespace RPC.Core.Services.Tests;

public class ReadServiceTests
{
    [Fact]
    internal void ReadFromNetwork_ExpectedJson()
    {
        var rpcConnection = "http://localhost:8545/";
        var response = new JObject()
        {
            { "jsonrpc", "2.0" },
            { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
            { "id", 0 }
        };
        var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(rpcConnection)
            .RespondWithJson(response);

        var result = new ReadService(rpcConnection).ReadFromNetwork("0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");

        Assert.NotNull(result);
        Assert.Equal(response, result);
    }
}
