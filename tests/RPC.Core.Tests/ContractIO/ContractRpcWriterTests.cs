using Xunit;
using Nethereum.Web3;
using RPC.Core.Models;
using RPC.Core.Builders;
using RPC.Core.Tests.Mocks;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcWriterTests
{
    private readonly RpcRequest request;

    public ContractRpcWriterTests()
    {
        request = new RpcRequest(
            rpcUrl: "http://localhost:8545/",
            to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            writeRequest: new WriteRpcRequest(
                chainId: 1,
                value: new HexBigInteger(10000000000000000),
                gasSettings: new GasSettings(30000, 6),
                accountProvider: MockMnemonicProvider.MnemonicProvider
            )
        );
    }

    [Fact]
    internal void InitializeWeb3()
    {
        var contractRpcWriter = new ContractRpcWriter(request);

        var result = contractRpcWriter.InitializeWeb3();

        Assert.NotNull(result);
        Assert.IsType<Web3>(result);
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
