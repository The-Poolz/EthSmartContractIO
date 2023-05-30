using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

public class WriteRequestValidator : AbstractValidator<Request>
{
    public WriteRequestValidator()
    {
        RuleFor(x => x.Web3)
            .NotNull();

        RuleFor(x => x.ChainId)
            .NotEqual(default(uint));

        RuleFor(x => x.From)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.To)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Value)
            .NotNull();

        RuleFor(x => x.GasSettings)
            .NotNull();

        RuleFor(x => x.GasSettings)
            .SetValidator(new GasSettingsValidator());
    }
}
