using FluentValidation;

namespace EthSmartContractIO.Models.Validation;

/// <summary>
/// Validator for write requests. It validates the <see cref="RpcRequest.WriteRequest"/> property to ensure it's not null,
/// and then validates the <see cref="WriteRpcRequest.ChainId"/>, <see cref="WriteRpcRequest.Value"/>, and <see cref="WriteRpcRequest.GasSettings"/> properties of the <see cref="RpcRequest.WriteRequest"/>.
/// </summary>
public class WriteRequestValidator : BaseRequestValidator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WriteRequestValidator"/> class.
    /// </summary>
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
