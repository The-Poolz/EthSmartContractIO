using Xunit;
using RPC.Core.Models;
using FluentValidation.TestHelper;

namespace RPC.Core.Validation.Tests;

public class GasSettingsValidatorTests
{
    private readonly GasSettingsValidator validator = new();

    [Fact]
    public void ShouldHaveError_WhenMaxGasLimitIsDefault()
    {
        var gasSettings = new GasSettings
        {
            MaxGasLimit = default,
            MaxGweiGasPrice = 50
        };

        var result = validator.TestValidate(gasSettings);

        result.ShouldHaveValidationErrorFor(x => x.MaxGasLimit);
    }

    [Fact]
    public void ShouldHaveError_WhenMaxGweiGasPriceIsDefault()
    {
        var gasSettings = new GasSettings
        {
            MaxGasLimit = 21000,
            MaxGweiGasPrice = default
        };

        var result = validator.TestValidate(gasSettings);

        result.ShouldHaveValidationErrorFor(x => x.MaxGweiGasPrice);
    }

    [Fact]
    public void ShouldNotHaveError_WhenMaxGasLimitIsNotDefault()
    {
        var gasSettings = new GasSettings
        {
            MaxGasLimit = 21000,
            MaxGweiGasPrice = 50
        };

        var result = validator.TestValidate(gasSettings);

        result.ShouldNotHaveValidationErrorFor(x => x.MaxGasLimit);
        result.ShouldNotHaveValidationErrorFor(x => x.MaxGweiGasPrice);
    }
}
