using Xunit;
using RPC.Core.Tests.Mocks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Managers.Tests;

public class GasManagerTests
{
    private readonly GasManager gasManager = new(MockWeb3.GetMock);

    [Fact]
    internal void EstimateGas_WithoutGasInTx_ExpectedGas()
    {
        var estimatedGas = new HexBigInteger(27109);
        var bufferOfGasLimit = new HexBigInteger(estimatedGas.Value / GasManager.GasBufferFactor);
        var expectedGas = new HexBigInteger(estimatedGas.Value + bufferOfGasLimit.Value);
        var transaction = new TransactionInput()
        {
            From = MockTransactionInput.MockTx.From,
            To = MockTransactionInput.MockTx.To,
            Value = MockTransactionInput.MockTx.Value,
            Data = MockTransactionInput.MockTx.Data,
        };

        var updatedTransaction = gasManager.EstimateGas(transaction);

        Assert.Equal(expectedGas, updatedTransaction.Gas);
    }

    [Fact]
    internal void EstimateGas_WithGasInTx_ThrowException()
    {
        var transaction = new TransactionInput()
        {
            From = MockTransactionInput.MockTx.From,
            To = MockTransactionInput.MockTx.To,
            Value = MockTransactionInput.MockTx.Value,
            Data = MockTransactionInput.MockTx.Data,
            Gas = new HexBigInteger(21000)
        };

        Action testCode = () => gasManager.EstimateGas(transaction);

        var exception = Assert.Throws<RpcResponseException>(testCode);
        Assert.Equal($"gas required exceeds allowance ({transaction.Gas.Value}): eth_estimateGas", exception.Message);
    }

    [Fact]
    internal void GetCurrentWeiGasPrice_ExpectedGasPrice()
    {
        var gasPrice = gasManager.GetCurrentWeiGasPrice();

        Assert.Equal(new HexBigInteger(5000000000), gasPrice);
    }
}
