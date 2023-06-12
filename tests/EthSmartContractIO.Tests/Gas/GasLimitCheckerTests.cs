using Xunit;
using FluentValidation;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Models;
using FluentValidation.TestHelper;
using EthSmartContractIO.Gas.Validation;

namespace EthSmartContractIO.Gas.Tests;

public class GasLimitCheckerTests
{
    private readonly GasPriceCheckerValidator validator = new();

    [Fact]
    public void GasPriceWithinLimit_NoValidationErrors()
    {
        var gasSettings = new GasSettings(30000, 6);
        var transactionInput = new TransactionInput
        {
            Gas = new HexBigInteger(27109),
            GasPrice = new HexBigInteger(5000000000)
        };
        var gasPriceChecker = new GasPriceChecker(transactionInput, gasSettings);

        var result = validator.TestValidate(gasPriceChecker);

        result.ShouldNotHaveValidationErrorFor(x => x.MaxGweiGasPrice);
        result.ShouldNotHaveValidationErrorFor(x => x.GasPrice);
    }

    [Fact]
    public void GasPriceExceedsLimit_ValidationErrors()
    {
        var gasSettings = new GasSettings(30000, 6);
        var transactionInput = new TransactionInput
        {
            Gas = new HexBigInteger(27109),
            GasPrice = new HexBigInteger(10000000000)
        };

        var exception = Assert.Throws<ValidationException>(() => new GasPriceChecker(transactionInput, gasSettings));
        Assert.Contains("Gas price is too high.", exception.Message);
    }
}
