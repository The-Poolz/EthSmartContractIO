using Xunit;
using RPC.Core.Models;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcReaderTests
{
    private const string RpcUrl = "http://localhost:8545/";
    private readonly string response = new JObject()
    {
        { "jsonrpc", "2.0" },
        { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
        { "id", 0 }
    }.ToString();

    [Fact]
    internal void RunContractAction_ShouldReturnExpectedJsonString()
    {
        var request = new RpcRequest(RpcUrl, "0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .RespondWithJson(response);

        var result = new ContractRpcReader(request).RunContractAction();

        Assert.NotNull(result);
        Assert.Equal(response, result);
    }
}
