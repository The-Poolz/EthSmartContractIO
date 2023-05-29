using Moq;
using Xunit;
using RPC.Core.Types;
using RPC.Core.Models;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcTests
{
    private readonly ContractRpc contractRpc = new();
    private const string RpcUrl = "http://localhost:8545/";
    private readonly string response = new JObject()
    {
        { "jsonrpc", "2.0" },
        { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
        { "id", 0 }
    }.ToString();

    [Fact]
    internal void Execute_InvalidActionType_ThrowException()
    {
        var mockActionInput = new Mock<IActionInput>();
        mockActionInput
            .SetupGet(x => x.ActionType)
            .Returns((ActionType)123);
        var mockRpcAction = new Mock<IRpcAction<IActionInput>>();

        Action testCode = () => contractRpc.Execute(mockRpcAction.Object, mockActionInput.Object);

        var exception = Assert.Throws<NotSupportedException>(testCode);
        Assert.Equal($"Unsupported ActionType: {(ActionType)123}", exception.Message);
    }

    [Fact]
    internal void Execute_Read_Expected()
    {
        var request = new RpcRequest("0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .WithRequestJson(JToken.FromObject(request))
            .RespondWithJson(response);
        var mockRpcAction = new Mock<IRpcAction<IActionInput>>();
        mockRpcAction.Setup(x => x.ExecuteAction(request))
            .Returns(response);

        var result = contractRpc.Execute(mockRpcAction.Object, request);

        Assert.NotNull(result);
        Assert.Equal(response, result);
    }

    [Fact]
    internal void Execute_Write_Expected()
    {
        var mockRpcAction = new Mock<IRpcAction<IActionInput>>();
        mockRpcAction.Setup(x => x.ExecuteAction(MockTransactionInput.MockTx))
            .Returns(response);

        var result = contractRpc.Execute(mockRpcAction.Object, MockTransactionInput.MockTx);

        Assert.NotNull(result);
        Assert.Equal(response, result);
    }
}
