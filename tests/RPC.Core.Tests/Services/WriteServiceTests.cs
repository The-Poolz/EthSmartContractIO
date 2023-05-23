using Xunit;
using RPC.Core.Tests.Mocks;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Services.Tests;

public class WriteServiceTests
{
    [Fact]
    internal void CreateWeb3()
    {
        string rpcConnection = "http://localhost:8545/";
        Account account = new("0x1");

        var web3 = WriteService.CreateWeb3(rpcConnection, account);

        Assert.NotNull(web3);
    }

    [Fact]
    internal void WriteToNetwork()
    {
        var writeService = new WriteService(MockWeb3.GetMock);

        var txHash = writeService.WriteToNetwork(MockTransactionInput.MockTx);

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }
}
