using Xunit;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Gas;
using EthSmartContractIO.Models;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.ContractIO.Tests;

public class ServiceManagerTests
{
    [Fact]
    internal void ServiceManager_ExpectedBackupServiceProvider()
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

        var result = new ServiceManager(request, null);

        Assert.NotNull(result);
        Assert.NotNull(result.GetService(typeof(GasPricer)));
        Assert.Equal(1, result.Account.ChainId);
    }

    [Fact]
    internal void ServiceManager_ExpectedPrimaryServiceProvider()
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
        var serviceProvider = new ServiceProviderBuilder()
            .AddAccountProvider(new MockAccountProvider())
            .Build();

        var result = new ServiceManager(request, serviceProvider);

        Assert.NotNull(result);
        Assert.NotNull(result.Account);
        Assert.Equal(1, result.Account.ChainId);
    }
}
