using FluentValidation;

namespace EthSmartContractIO.Models.Validation;

/// <summary>
/// Validator for read requests. It validates the <see cref="RpcRequest.Data"/> property to ensure it's correctly formatted,
/// and the <see cref="RpcRequest.WriteRequest"/> property to ensure it's null.
/// </summary>
public class ReadRequestValidator : BaseRequestValidator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadRequestValidator"/> class.
    /// </summary>
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
