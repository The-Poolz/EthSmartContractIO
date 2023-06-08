using Moq;
using Xunit;
using RPC.Core.Gas;
using RPC.Core.Models;
using RPC.Core.Builders;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;
using RPC.Core.Tests.Mocks;
using RPC.Core.Transaction;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcTests
{
    private const string RpcUrl = "http://localhost:8545/";
    private readonly ContractRpc contractRpc = new();
    private readonly RpcRequest readRequest = new (
        rpcUrl: RpcUrl,
        to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
        data: "0xbef7a2f0"
    );
    private readonly RpcRequest writeRequest = new (
        rpcUrl: "http://localhost:8545/",
        to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
        writeRequest: new WriteRpcRequest(
            chainId: 1,
            value: new HexBigInteger(10000000000000000),
            gasSettings: new GasSettings(30000, 6),
            accountProvider: new MockAccountProvider()
        )
    );

    [Fact]
    internal void ExecuteAction_Read_ExpectedJsonString()
    {
        var response = new JObject()
        {
            { "jsonrpc", "2.0" },
            { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
            { "id", 0 }
        };
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .RespondWithJson(response);

        var result = contractRpc.ExecuteAction(readRequest);

        Assert.NotNull(result);
        Assert.Equal(response["result"]?.ToString(), result);
    }

    [Fact]
    internal void ExecuteAction_WriteWithMockServices_ExpectedTransactionHex()
    {
        var mockGasPricer = new Mock<IGasPricer>();
        mockGasPricer.Setup(x => x.GetCurrentWeiGasPrice())
            .Returns(new HexBigInteger(5000000000));
        var mockTransactionSigner = new Mock<ITransactionSigner>();
        mockTransactionSigner.Setup(x => x.SignTransaction(It.IsAny<TransactionInput>()))
            .Returns("signedTransaction");
        var mockTransactionSender = new Mock<ITransactionSender>();
        mockTransactionSender.Setup(x => x.SendTransaction("signedTransaction"))
            .Returns("transactionHash");
        contractRpc.ServiceProvider = new ServiceProviderBuilder()
            .AddGasPricer(mockGasPricer.Object)
            .AddTransactionSigner(mockTransactionSigner.Object)
            .AddTransactionSender(mockTransactionSender.Object)
            .Build();

        var result = contractRpc.ExecuteAction(writeRequest);

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }

    [Fact]
    internal void ExecuteAction_WriteWithMockWeb3_ExpectedTransactionHex()
    {
        contractRpc.ServiceProvider = new ServiceProviderBuilder()
            .AddWeb3(MockWeb3.GetMock)
            .Build();

        var result = contractRpc.ExecuteAction(writeRequest);

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }
}
