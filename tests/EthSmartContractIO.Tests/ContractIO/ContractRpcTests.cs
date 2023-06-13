using Moq;
using Xunit;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using EthSmartContractIO.Gas;
using EthSmartContractIO.Models;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Tests.Mocks;
using EthSmartContractIO.Transaction;

namespace EthSmartContractIO.ContractIO.Tests;

public class ContractRpcTests
{
    private const string RpcUrl = "http://localhost:8545/";
    private readonly RpcRequest readRequest = new (
        rpcUrl: RpcUrl,
        to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
        data: "0xbef7a2f0"
    );
    private readonly RpcRequest writeRequest = new (
        rpcUrl: "http://localhost:8545/",
        to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
        writeRequest: new WriteRpcRequest(
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

        var result = new ContractIO().ExecuteAction(readRequest);

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
        var serviceProvider = new ServiceProviderBuilder()
            .AddGasPricer(mockGasPricer.Object)
            .AddTransactionSigner(mockTransactionSigner.Object)
            .AddTransactionSender(mockTransactionSender.Object)
            .Build();

        var result = new ContractIO(serviceProvider).ExecuteAction(writeRequest);

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }

    [Fact]
    internal void ExecuteAction_WriteWithMockWeb3_ExpectedTransactionHex()
    {
        var serviceProvider = new ServiceProviderBuilder()
            .AddWeb3(MockWeb3.GetMock)
        .Build();

        var result = new ContractIO(serviceProvider).ExecuteAction(writeRequest);

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }
}
