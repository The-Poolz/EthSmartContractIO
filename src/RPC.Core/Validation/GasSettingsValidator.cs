using FluentValidation;
using EthSmartContractIO.Models;

namespace EthSmartContractIO.Validation;

public class GasSettingsValidator : AbstractValidator<GasSettings>
{
    public GasSettingsValidator()
    {
        RuleFor(x => x.MaxGasLimit)
            .NotEqual(default(uint));

        RuleFor(x => x.MaxGweiGasPrice)
            .NotEqual(default(uint));
    }
}
