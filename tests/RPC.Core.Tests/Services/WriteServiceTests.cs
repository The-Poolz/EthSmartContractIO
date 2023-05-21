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
        var writeService = new WriteService(MockWeb3.GetMock, MockContractManager.ABI, MockContractManager.ContractAddress);

        var txHash = writeService.WriteToNetwork(
            MockTransactionInput.MockTx.ChainId,
            MockTransactionInput.MockTx.From,
            MockContractManager.ContractAddress,
            MockTransactionInput.MockTx.Value,
            MockTransactionInput.MockTx.Gas,
            MockTransactionInput.MockTx.GasPrice,
            "SignUp",
            new object[] { 1 }
        );

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }
}
