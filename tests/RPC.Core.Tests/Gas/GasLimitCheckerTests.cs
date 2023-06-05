using Xunit;
using RPC.Core.Models;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using RPC.Core.Gas.Exceptions;

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

        new GasLimitChecker(transactionInput, gasSettings).CheckAndThrow();
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

        Action testCode = () => new GasLimitChecker(transactionInput, gasSettings).CheckAndThrow();

        var exception = Assert.Throws<GasLimitExceededException>(testCode);
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

        Action testCode = () => new GasLimitChecker(transactionInput, gasSettings).CheckAndThrow();

        var exception = Assert.Throws<GasPriceExceededException>(testCode);
        Assert.Equal("Gas price exceeded.", exception.Message);
    }
}
