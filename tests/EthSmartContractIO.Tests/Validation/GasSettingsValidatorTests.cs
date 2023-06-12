using Xunit;
using EthSmartContractIO.Models;
using FluentValidation.TestHelper;
using EthSmartContractIO.Models.Validation;

namespace EthSmartContractIO.Validation.Tests;

public class GasSettingsValidatorTests
{
    private readonly GasSettingsValidator validator = new();

    [Fact]
    public void ShouldHaveError_WhenMaxGasLimitIsDefault()
    {
        var gasSettings = new GasSettings(default, 50);

        var result = validator.TestValidate(gasSettings);

        result.ShouldHaveValidationErrorFor(x => x.MaxGasLimit);
    }

    [Fact]
    public void ShouldHaveError_WhenMaxGweiGasPriceIsDefault()
    {
        var gasSettings = new GasSettings(21000, default);

        var result = validator.TestValidate(gasSettings);

        result.ShouldHaveValidationErrorFor(x => x.MaxGweiGasPrice);
    }

    [Fact]
    public void ShouldNotHaveError_WhenMaxGasLimitIsNotDefault()
    {
        var gasSettings = new GasSettings(21000, 50);

        var result = validator.TestValidate(gasSettings);

        result.ShouldNotHaveValidationErrorFor(x => x.MaxGasLimit);
        result.ShouldNotHaveValidationErrorFor(x => x.MaxGweiGasPrice);
    }
}
