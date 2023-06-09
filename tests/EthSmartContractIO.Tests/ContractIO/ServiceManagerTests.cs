using Xunit;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Models;
using EthSmartContractIO.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace EthSmartContractIO.ContractIO.Tests;

public class ServiceManagerTests
{
    [Fact]
    internal void ServiceManager_ExpectedServiceProvider()
    {
        var request = new RpcRequest(
            rpcUrl: "http://localhost:8545/",
            to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            writeRequest: new WriteRpcRequest(
                chainId: 1,
                value: new HexBigInteger(10000000000000000),
                gasSettings: new GasSettings(30000, 6),
                accountProvider: new MockAccountProvider()
            )
        );

        var expectedServiceManager = new ServiceManager(request, null);

        var result = expectedServiceManager.GetService<IWeb3>();

        Assert.NotNull(expectedServiceManager);
        Assert.NotNull(result);
        Assert.IsType<Web3>(result);
    }
}
