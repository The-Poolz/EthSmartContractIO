using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Transaction.Tests;

public class TransactionSenderTests
{
    [Fact]
    internal void SignTransaction_ExpectedTransaction()
    {
        var manager = new TransactionSender(MockWeb3.GetMock);

        var txHash = manager.SendTransaction("signedTransaction");

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }
}
