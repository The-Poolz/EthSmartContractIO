using Xunit;
using RPC.Core.Models;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Gas.Tests;

public class GasLimitCheckerTests
{
    [Fact]
    internal void CheckGasLimits_LimitsNotExceeded_WithoutThrows()
    {
        var gasSettings = new GasSettings(30000, 6);
        var transactionInput = new TransactionInput()
        {
            Gas = new HexBigInteger(27109),
            GasPrice = new HexBigInteger(5000000000)
        };

        GasLimitChecker.CheckGasLimits(transactionInput, gasSettings);
    }

    [Fact]
    internal void CheckGasLimits_GasLimitExceeded_ThrowException()
    {
        var gasSettings = new GasSettings(20000, 6);
        var transactionInput = new TransactionInput()
        {
            Gas = new HexBigInteger(27109),
            GasPrice = new HexBigInteger(5000000000)
        };

        void testCode() => GasLimitChecker.CheckGasLimits(transactionInput, gasSettings);

        var exception = Assert.Throws<InvalidOperationException>(testCode);
        Assert.Equal("Gas limit exceeded.", exception.Message);
    }

    [Fact]
    internal void CheckGasLimits_GasPriceExceeded_ThrowException()
    {
        var gasSettings = new GasSettings(30000, 4);
        var transactionInput = new TransactionInput()
        {
            Gas = new HexBigInteger(27109),
            GasPrice = new HexBigInteger(5000000000)
        };

        void testCode() => GasLimitChecker.CheckGasLimits(transactionInput, gasSettings);

        var exception = Assert.Throws<InvalidOperationException>(testCode);
        Assert.Equal("Gas price exceeded.", exception.Message);
    }
}
