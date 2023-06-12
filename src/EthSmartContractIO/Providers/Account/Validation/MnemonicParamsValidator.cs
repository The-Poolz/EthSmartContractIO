using FluentValidation;

namespace EthSmartContractIO.Providers.Account.Validation;

public class MnemonicParamsValidator : AbstractValidator<object[]>
{
    public MnemonicParamsValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Length >= 3)
            .WithMessage("Three arguments required: mnemonic words, account ID and chain ID.");

        RuleFor(x => x[0])
            .NotNull()
            .NotEmpty()
            .Must(x => x is string)
            .WithMessage("The first argument must be a (string) mnemonic words.");

        RuleFor(x => x[1])
            .NotNull()
            .NotEmpty()
            .Must(x => x is uint)
            .NotEqual(default(uint))
            .WithMessage("The second argument must be a (uint) account ID");

        RuleFor(x => x[2])
            .NotNull()
            .NotEmpty()
            .Must(x => x is uint)
            .NotEqual(default(uint))
            .WithMessage("The third argument must be a (uint) chain ID");
    }
}
