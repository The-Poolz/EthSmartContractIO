using FluentValidation;

namespace EthSmartContractIO.Providers.Account.Validation;

public class PrivateKeyParamsValidator : AbstractValidator<object[]>
{
    public PrivateKeyParamsValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Length == 2)
            .WithMessage("Two arguments required: private key and chain ID.");

        RuleFor(x => x[0])
            .NotNull()
            .NotEmpty()
            .Must(x => x is string)
            .WithMessage("The first argument must be a (string) private key.");

        RuleFor(x => x[1])
            .NotNull()
            .NotEmpty()
            .Must(x => x is uint)
            .NotEqual(default(uint))
            .WithMessage("The second argument must be a (uint) chain ID");
    }
}
