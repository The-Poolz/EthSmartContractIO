using FluentValidation;

namespace EthSmartContractIO.Gas.Validation;

/// <summary>
/// Validator for gas price checks. It validates that the <see cref="GasPriceChecker.GasPrice"/> is not greater than the <see cref="GasPriceChecker.MaxGweiGasPrice"/>.
/// </summary>
public class GasPriceCheckerValidator : AbstractValidator<GasPriceChecker>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GasPriceCheckerValidator"/> class.
    /// </summary>
    public GasPriceCheckerValidator()
    {
        RuleFor(x => x.MaxGweiGasPrice)
            .GreaterThanOrEqualTo(x => x.GasPrice)
            .WithMessage("Gas price is too high.");
    }
}