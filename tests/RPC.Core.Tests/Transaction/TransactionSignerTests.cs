using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Transaction.Tests;

public class TransactionSignerTests
{
    [Fact]
    internal void SignTransaction_ExpectedSignedTransaction()
    {
        var manager = new TransactionSigner(MockWeb3.GetMock);

        var signedTransaction = manager.SignTransaction(MockTransactionInput.MockTx);

        Assert.NotNull(signedTransaction);
        Assert.Equal("signedTransaction", signedTransaction);
    }
}
