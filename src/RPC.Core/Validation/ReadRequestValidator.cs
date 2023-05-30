using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

public class ReadRequestValidator : AbstractValidator<Request>
{
    public ReadRequestValidator()
    {
        RuleFor(x => x.RpcUrl)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.To)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Data)
            .NotNull()
            .NotEmpty();
    }
}
