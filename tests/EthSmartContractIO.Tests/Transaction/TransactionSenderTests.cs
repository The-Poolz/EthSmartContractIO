using Xunit;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.Transaction.Tests;

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
