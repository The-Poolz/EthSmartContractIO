using FluentValidation;

namespace RPC.Core.Validation;

public class ReadRequestValidator : BaseRequestValidator
{
    public ReadRequestValidator() : base()
    {
        RuleFor(x => x.Data)
            .NotEmpty()
            .Must(IsValidEthereumData)
            .WithMessage("Parameter 'Data' not correctly formatted.");
    }
}
