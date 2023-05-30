using RPC.Core.Types;
using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.To)
            .NotNull()
            .NotEmpty();

        When(x => x.ActionType == ActionType.Read, RequireReadParameters);

        When(x => x.ActionType == ActionType.Write, RequireWriteParameters);

        When(x => x.ActionType == ActionType.Read, EnsureWriteParametersAreNull);

        When(x => x.ActionType == ActionType.Write, EnsureReadParametersAreNull);
    }

    private void RequireReadParameters()
    {
        RuleFor(x => x.RpcUrl)
            .NotEmpty();

        RuleFor(x => x.Data)
            .NotEmpty();
    }

    private void RequireWriteParameters()
    {
        RuleFor(x => x.Web3)
            .NotNull();

        RuleFor(x => x.ChainId)
            .NotEqual(default(uint));

        RuleFor(x => x.From)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Value)
            .NotNull();

        RuleFor(x => x.GasSettings)
            .NotNull()
            .SetValidator(new GasSettingsValidator()!);
    }

    private void EnsureWriteParametersAreNull()
    {
        RuleFor(x => x.Web3)
            .Null()
            .WithMessage("Web3 must be null for Read operations");

        RuleFor(x => x.From)
            .Empty()
            .WithMessage("From must be null for Read operations");

        RuleFor(x => x.Value)
            .Null()
            .WithMessage("Value must be null for Read operations");

        RuleFor(x => x.GasSettings)
            .Null()
            .WithMessage("GasSettings must be null for Read operations");
    }

    private void EnsureReadParametersAreNull()
    {
        RuleFor(x => x.RpcUrl)
            .Empty()
            .WithMessage("RpcUrl must be null for Write operations");
    }
}
