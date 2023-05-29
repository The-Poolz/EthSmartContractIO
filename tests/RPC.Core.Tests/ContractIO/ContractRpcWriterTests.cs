using Xunit;
using RPC.Core.Tests.Mocks;
using Nethereum.Web3.Accounts;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcWriterTests
{
    [Fact]
    internal void CreateWeb3()
    {
        string rpcConnection = "http://localhost:8545/";
        Account account = new("0x1");

        var web3 = ContractRpcWriter.CreateWeb3(rpcConnection, account);

        Assert.NotNull(web3);
    }

    [Fact]
    internal void WriteToNetwork()
    {
        var writeService = new ContractRpcWriter(MockWeb3.GetMock);

        var txHash = writeService.ExecuteAction(MockTransactionInput.MockTx);

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }

    [Fact]
    internal void WriteToNetwork_ShouldReturnExpectedTransactionHash()
    {
        var writeService = new ContractRpcWriter(MockWeb3.GetMock);

        var txHash = writeService.WriteToNetwork(MockTransactionInput.MockTx);

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }
}
