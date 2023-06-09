using Xunit;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.Transaction.Tests;

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
