using Xunit;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Models;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.ContractIO.Tests;

public class ContractRpcWriterTests
{
    private readonly RpcRequest request;

    public ContractRpcWriterTests()
    {
        request = new RpcRequest(
            rpcUrl: "http://localhost:8545/",
            to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            writeRequest: new WriteRpcRequest(
                value: new HexBigInteger(10000000000000000),
                gasSettings: new GasSettings(30000, 6),
                accountParams: new object[] { "0x4e3c79ee2f53da4e456cb13887f4a7d59488677e9e48b6fb6701832df828f7e9", (uint)1 }
            )
        );
    }

    [Fact]
    internal void RunContractAction_ExpectedTransactionHex()
    {
        var serviceProvider = new ServiceProviderBuilder()
            .AddWeb3(MockWeb3.GetMock)
            .Build();
        var contractRpcWriter = new ContractRpcWriter(request, serviceProvider);

        var result = contractRpcWriter.RunContractAction();

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }
}
