using Xunit;
using Nethereum.RPC.Eth.DTOs;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.Transaction.Tests;

public class TransactionSignerTests
{
    [Fact]
    internal void SignTransaction_ExpectedSignedTransaction()
    {
        var manager = new TransactionSigner(MockWeb3.GetMock);
        var transaction = new TransactionInput();

        var signedTransaction = manager.SignTransaction(transaction);

        Assert.NotNull(signedTransaction);
        Assert.Equal("signedTransaction", signedTransaction);
    }
}
