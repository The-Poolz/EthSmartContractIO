using Xunit;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;
using EthSmartContractIO.Models;

namespace EthSmartContractIO.ContractIO.Tests;

public class ContractReaderTests
{
    private const string RpcUrl = "http://localhost:8545/";
    private readonly JObject response = new()
    {
        { "jsonrpc", "2.0" },
        { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
        { "id", 0 }
    };

    [Fact]
    internal void RunContractAction_ShouldReturnExpectedJsonString()
    {
        var request = new RpcRequest(RpcUrl, "0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .RespondWithJson(response);

        var result = new ContractReader(request).RunContractAction();

        Assert.NotNull(result);
        Assert.Equal(response["result"]?.ToString(), result);
    }

    [Fact]
    internal void RunContractAction_ShouldThrowException()
    {
        var errorResponse = new JObject
        {
            { "jsonrpc", "2.0" },
            { "id", 0 },
            {
                "error", new JObject
                {
                    { "code", -32602 },
                    { "message", "missing value for required argument 0" }
                }
            }
        };
        var request = new RpcRequest(RpcUrl, "0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .RespondWithJson(errorResponse);

        void testCode() => new ContractReader(request).RunContractAction();

        var exception = Assert.Throws<KeyNotFoundException>(testCode);
        Assert.Equal("Response does not contain the key 'result'.", exception.Message);
    }
}
