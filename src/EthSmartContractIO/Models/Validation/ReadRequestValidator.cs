using FluentValidation;

namespace EthSmartContractIO.Models.Validation;

public class ReadRequestValidator : BaseRequestValidator
{
    public ReadRequestValidator() : base()
    {
        RuleFor(x => x.Data)
            .NotEmpty()
            .Must(IsValidEthereumData)
            .WithMessage("Parameter 'Data' not correctly formatted.");

        RuleFor(x => x.WriteRequest)
            .Null()
            .WithMessage("Parameter 'WriteRequest' must be null.");
    }
}
