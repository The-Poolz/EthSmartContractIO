using Nethereum.Util;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EthSmartContractIO.Models.Validation;

/// <summary>
/// Base class for validating RPC requests.<br/>
/// It validates the <see cref="RpcRequest.To"/> property to ensure it's a valid Ethereum address,
/// and the <see cref="RpcRequest.RpcUrl"/> property to ensure it's a valid URL.
/// </summary>
public class BaseRequestValidator : AbstractValidator<RpcRequest>
{
    protected const string MethodSignaturePattern = @"^0x[0-9a-fA-F]{8}";
    protected const string ParametersPattern = @"([0-9a-fA-F]{64})*$";
    protected static readonly Regex EthereumDataPattern = new($"{MethodSignaturePattern}{ParametersPattern}", default, TimeSpan.FromMinutes(1));

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRequestValidator"/> class.
    /// </summary>
    public BaseRequestValidator()
    {
        RuleFor(x => x.To)
            .NotNull()
            .NotEmpty()
            .Must(IsValidEthereumAddress)
            .WithMessage(GetInvalidAddressMessage("To"));

        RuleFor(x => x.RpcUrl)
            .NotNull()
            .NotEmpty()
            .Must(Flurl.Url.IsValid)
            .WithMessage("Invalid URL.");
    }

    protected static bool IsValidEthereumAddress(string address) =>
        address.IsValidEthereumAddressHexFormat();

    protected static bool IsValidEthereumData(string data) =>
        EthereumDataPattern.IsMatch(data);

    protected static string GetInvalidAddressMessage(string fieldName) =>
        $"Parameter '{fieldName}' is invalid ethereum address.";
}
