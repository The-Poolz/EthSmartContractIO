using FluentValidation;

namespace EthSmartContractIO.Models.Validation;

/// <summary>
/// Validator for gas settings.<br/>
/// It validates the <see cref="GasSettings.MaxGasLimit"/> and <see cref="GasSettings.MaxGweiGasPrice"/> properties to ensure they are not default values.
/// </summary>
public class GasSettingsValidator : AbstractValidator<GasSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GasSettingsValidator"/> class.
    /// </summary>
    public GasSettingsValidator()
    {
        RuleFor(x => x.MaxGasLimit)
            .NotEqual(default(uint));

        RuleFor(x => x.MaxGweiGasPrice)
            .NotEqual(default(uint));
    }
}
