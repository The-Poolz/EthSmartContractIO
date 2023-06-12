using Xunit;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Models;

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
                value: new HexBigInteger(10000000000000000),
                gasSettings: new GasSettings(30000, 6),
                accountParams: new object[] { "0x4e3c79ee2f53da4e456cb13887f4a7d59488677e9e48b6fb6701832df828f7e9", (uint)1 }
            )
        );

        var expectedServiceManager = new ServiceManager(request, null);

        var result = expectedServiceManager.Web3;

        Assert.NotNull(expectedServiceManager);
        Assert.NotNull(result);
        Assert.IsType<Web3>(result);
    }
}
