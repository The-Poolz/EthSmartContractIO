using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Managers.Tests;

public class TransactionManagerTests
{
    [Fact]
    internal void SignTransaction_ExpectedSignedTransaction()
    {
        var manager = new TransactionManager(MockWeb3.GetMock);

        var signedTransaction = manager.SignTransaction(MockTransactionInput.MockTx);

        Assert.NotNull(signedTransaction);
        Assert.Equal("signedTransaction", signedTransaction);
    }

    [Fact]
    internal void SignTransaction_ExpectedTransaction()
    {
        var manager = new TransactionManager(MockWeb3.GetMock);

        var txHash = manager.SendTransaction("signedTransaction");

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }
}
