using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

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
