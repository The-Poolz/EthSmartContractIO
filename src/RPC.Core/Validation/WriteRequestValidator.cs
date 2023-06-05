using FluentValidation;

namespace RPC.Core.Validation;

public class WriteRequestValidator : BaseRequestValidator
{
    public WriteRequestValidator() : base()
    {
        RuleFor(x => x.ChainId)
            .NotEqual(default(uint));

        RuleFor(x => x.Value)
            .NotNull();

        RuleFor(x => x.GasSettings)
            .SetValidator(new GasSettingsValidator());
    }
}
