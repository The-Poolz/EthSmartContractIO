using RPC.Core.Types;
using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.RpcUrl)
            .NotEmpty()
            .When(x => x.ActionType == ActionType.Read);

        RuleFor(x => x.Web3)
            .NotNull()
            .When(x => x.ActionType == ActionType.Write);

        RuleFor(x => x.To)
            .NotNull().NotEmpty();

        RuleFor(x => x.Data)
            .NotEmpty()
            .When(x => x.ActionType == ActionType.Read);

        RuleFor(x => x.ChainId)
            .NotEqual(default(uint))
            .When(x => x.ActionType == ActionType.Write);

        RuleFor(x => x.From)
            .NotNull()
            .NotEmpty()
            .When(x => x.ActionType == ActionType.Write);

        RuleFor(x => x.Value)
            .NotNull()
            .When(x => x.ActionType == ActionType.Write);

        RuleFor(x => x.GasSettings)
            .NotNull()
            .SetValidator(new GasSettingsValidator())
            .When(x => x.ActionType == ActionType.Write);
    }
}
