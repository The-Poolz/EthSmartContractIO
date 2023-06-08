using Xunit;
using RPC.Core.Models;
using RPC.Core.Builders;
using RPC.Core.Tests.Mocks;
using Nethereum.Hex.HexTypes;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Web3;

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
                accountProvider: new MockAccountProvider()
            )
        );
    }

    [Fact]
    internal void ServiceManager_ExpectedServiceProvider()
    {
        var expectedServiceManager = new ServiceManager(request, null);

        var result = expectedServiceManager.GetService<IWeb3>();

        Assert.NotNull(expectedServiceManager);
        Assert.NotNull(result);
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
