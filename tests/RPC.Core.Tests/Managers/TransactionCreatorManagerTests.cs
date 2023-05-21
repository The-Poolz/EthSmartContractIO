using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Managers.Tests;

public class TransactionCreatorManagerTests
{
    [Fact]
    internal void CreateTransactionInput_ExpectedTransactionInput()
    {
        var transactionInput = TransactionCreatorManager.CreateTransactionInput(
            MockTransactionInput.MockTx.ChainId,
            MockTransactionInput.MockTx.From,
            MockContractManager.ContractAddress,
            MockTransactionInput.MockTx.Value,
            MockTransactionInput.MockTx.Gas,
            MockTransactionInput.MockTx.GasPrice,
            MockContractManager.ContractManager.GetMethod("SignUp"),
            new object[] { 1 }
        );

        Assert.NotNull(transactionInput);
        Assert.Equal(MockTransactionInput.MockTx.ChainId, transactionInput.ChainId);
        Assert.Equal(MockTransactionInput.MockTx.Data, transactionInput.Data);
        Assert.Equal(MockTransactionInput.MockTx.From, transactionInput.From);
        Assert.Equal(MockTransactionInput.MockTx.Gas, transactionInput.Gas);
        Assert.Equal(MockTransactionInput.MockTx.GasPrice, transactionInput.GasPrice);
        Assert.Null(transactionInput.MaxFeePerGas);
        Assert.Null(transactionInput.MaxPriorityFeePerGas);
        Assert.Null(transactionInput.Nonce);
        Assert.Equal(MockContractManager.ContractAddress, transactionInput.To);
        Assert.Null(transactionInput.Type);
        Assert.Null(transactionInput.Value);
    }
}
