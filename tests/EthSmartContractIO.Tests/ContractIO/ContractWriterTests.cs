using Xunit;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Models;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.ContractIO.Tests;

public class ContractWriterTests
{
    private readonly RpcRequest request;

    public ContractWriterTests()
    {
        request = new RpcRequest(
            rpcUrl: "http://localhost:8545/",
            to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            writeRequest: new WriteRpcRequest(
                value: new HexBigInteger(10000000000000000),
                gasSettings: new GasSettings(30000, 6),
                accountProvider: new MockAccountProvider()
            )
        );
    }

    [Fact]
    internal void RunContractAction_ExpectedTransactionHex()
    {
        var serviceProvider = new ServiceProviderBuilder()
            .AddWeb3(MockWeb3.GetMock)
            .Build();

        var result = new ContractWriter(request, serviceProvider).RunContractAction();

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }
}
