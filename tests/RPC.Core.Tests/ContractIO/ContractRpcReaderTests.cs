using Xunit;
using RPC.Core.Models;
using FluentValidation;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcReaderTests
{
    private const string RpcUrl = "http://localhost:8545/";
    private readonly JObject response = new()
    {
        { "jsonrpc", "2.0" },
        { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
        { "id", 0 }
    };

    [Fact]
    internal void ReadFromNetwork_ShouldReturnExpectedJson()
    {
        var request = new RpcRequest("0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .WithRequestJson(JToken.FromObject(request))
            .RespondWithJson(response);

        var result = new ContractRpcReader(RpcUrl).ReadFromNetwork(request);

        Assert.NotNull(result);
        Assert.Equal(response, result);
    }

    [Theory]
    [InlineData("0xbef7a2f0")]
    [InlineData("0x2417a19b0000000000000000000000000000000000000000000000000000000000000001")]
    [InlineData("0x2417a19b000000000000000000000000A98b8386a806966c959C35c636b929FE7c5dD7dE0000000000000000000000000000000000000000000000000000000000000001")]
    internal void CheckValidator_DataBeValid_ShouldNotThrowException(string data)
    {
        var request = new RpcRequest("0xA98b8386a806966c959C35c636b929FE7c5dD7dE", data);
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .WithRequestJson(JToken.FromObject(request))
            .RespondWithJson(response);

        var result = new ContractRpcReader(RpcUrl).ReadFromNetwork(request);

        Assert.NotNull(result);
        Assert.Equal(response, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("0x")]
    [InlineData("A98b8386a806966c959C35c636b929FE7c5dD7dE")]
    internal void CheckValidator_ToParameter_ShouldThrowException(string to)
    {
        var request = new RpcRequest(to, "0xbef7a2f0");

        Action testCode = () => new ContractRpcReader(RpcUrl).ReadFromNetwork(request);

        var exception = Assert.Throws<ValidationException>(testCode);
        Assert.Equal(GetExceptionMessage("to"), exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("0x")]
    [InlineData("0x(*&^%#$@")]
    [InlineData("0xbef7a2f")]
    [InlineData("0xbef7a2fFF")]
    [InlineData("0x2417a19b000000000000000000000000000000000000000000000000000000000000000")]
    internal void CheckValidator_DataParameter_ShouldThrowException(string data)
    {
        var request = new RpcRequest("0xA98b8386a806966c959C35c636b929FE7c5dD7dE", data);

        Action testCode = () => new ContractRpcReader(RpcUrl).ReadFromNetwork(request);

        var exception = Assert.Throws<ValidationException>(testCode);
        Assert.Equal(GetExceptionMessage("data"), exception.Message);
    }

    private static string GetExceptionMessage(string parameterName) =>
            $"Validation failed: {Environment.NewLine} -- : Parameter '{parameterName}' is empty or not correctly formatted. Severity: Error";
}
