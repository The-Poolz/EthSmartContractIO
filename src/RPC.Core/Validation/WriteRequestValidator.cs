using FluentValidation;

namespace RPC.Core.Validation;

public class WriteRequestValidator : BaseRequestValidator
{
    public WriteRequestValidator() : base()
    {
        RuleFor(x => x.WriteRequest)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(x => x.WriteRequest!.ChainId)
                    .NotEqual(default(uint));

                RuleFor(x => x.WriteRequest!.Value)
                    .NotNull();

                RuleFor(x => x.WriteRequest!.GasSettings)
                    .NotNull()
                    .SetValidator(new GasSettingsValidator());
            });
    }
}
