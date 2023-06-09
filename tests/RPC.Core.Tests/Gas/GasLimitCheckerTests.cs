using Xunit;
using RPC.Core.Models;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using RPC.Core.Validation;
using FluentValidation;

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

        new GasPriceCheckerValidator().ValidateAndThrow
            (new GasPriceChecker(transactionInput, gasSettings));
    }
}
