using Xunit;
using RPC.Core.Tests.Mocks;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Gas.Tests;

public class GasEstimatorTests
{
    private readonly GasEstimator gasEstimator = new(MockWeb3.GetMock);

    [Fact]
    internal void EstimateGas_WithoutGasInTx_ExpectedGas()
    {
        var estimatedGas = new HexBigInteger(27109);
        var bufferOfGasLimit = new HexBigInteger(estimatedGas.Value / GasEstimator.GasBufferFactor);
        var expectedGas = new HexBigInteger(estimatedGas.Value + bufferOfGasLimit.Value);
        var transaction = new TransactionInput()
        {
            From = MockTransactionInput.MockTx.From,
            To = MockTransactionInput.MockTx.To,
            Value = MockTransactionInput.MockTx.Value,
            Data = MockTransactionInput.MockTx.Data,
        };

        var updatedTransaction = gasEstimator.EstimateGas(transaction);

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

        Action testCode = () => gasEstimator.EstimateGas(transaction);

        var exception = Assert.Throws<RpcResponseException>(testCode);
        Assert.Equal($"gas required exceeds allowance ({transaction.Gas.Value}): eth_estimateGas", exception.Message);
    }

    [Fact]
    internal void EstimateGas_RejectedByContract_ThrowException()
    {
        var transaction = new TransactionInput()
        {
            From = MockTransactionInput.MockTx.From,
            To = MockTransactionInput.MockTx.To,
            Value = new HexBigInteger(1000000000000000),
            Data = MockTransactionInput.MockTx.Data
        };

        Action testCode = () => gasEstimator.EstimateGas(transaction);

        var exception = Assert.Throws<RpcResponseException>(testCode);
        Assert.Equal($"execution reverted: Not Enough Fee Provided: eth_estimateGas", exception.Message);
    }
}
