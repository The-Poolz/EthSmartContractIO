using Moq;
using Xunit;
using Nethereum.Web3;
using RPC.Core.Managers;
using RPC.Core.Tests.Mocks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.Services;

namespace RPC.Core.Tests;

public class TransactionManagerTests
{
    private readonly IWeb3 web3;

    public TransactionManagerTests()
    {
        var clientMock = new Mock<IClient>();

        var transactionManagerMock = new Mock<Nethereum.RPC.TransactionManagers.TransactionManager>(clientMock.Object);
        var accountMock = new Mock<Account>();

        transactionManagerMock.Setup(tm => tm.SignTransactionAsync(It.IsAny<TransactionInput>()))
            .ReturnsAsync("signedTransaction");

        var ethApiTransactionsServiceMock = new Mock<IEthApiTransactionsService>();
        ethApiTransactionsServiceMock.Setup(tr => tr.SendRawTransaction.SendRequestAsync(It.IsAny<string>(), null))
            .ReturnsAsync("transactionHash");

        var web3Mock = new Mock<IWeb3>();
        web3Mock.SetupGet(w => w.TransactionManager.Account.TransactionManager).Returns(transactionManagerMock.Object);
        web3Mock.SetupGet(w => w.Eth.Transactions).Returns(ethApiTransactionsServiceMock.Object);

        web3 = web3Mock.Object;
    }

    [Fact]
    internal void SignTransaction_ExpectedSignedTransaction()
    {
        var manager = new TransactionManager(web3);

        var signedTransaction = manager.SignTransaction(MockTransactionInput.MockTx);

        Assert.NotNull(signedTransaction);
        Assert.Equal("signedTransaction", signedTransaction);
    }

    [Fact]
    internal void SignTransaction_ExpectedTransaction()
    {
        var manager = new TransactionManager(web3);

        var txHash = manager.SendTransaction("signedTransaction");

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }
}
