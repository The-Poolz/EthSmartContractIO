using Nethereum.Util;
using RPC.Core.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace RPC.Core.Validation;

public class BaseRequestValidator : AbstractValidator<RpcRequest>
{
    protected const string MethodSignaturePattern = @"^0x[0-9a-fA-F]{8}";
    protected const string ParametersPattern = @"([0-9a-fA-F]{64})*$";
    protected static readonly Regex EthereumDataPattern = new($"{MethodSignaturePattern}{ParametersPattern}", default, TimeSpan.FromMinutes(1));

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
        AddressExtensions.IsValidEthereumAddressHexFormat(address);

    protected static bool IsValidEthereumData(string data) =>
        EthereumDataPattern.IsMatch(data);

    protected static string GetInvalidAddressMessage(string fieldName) =>
        $"Parameter '{fieldName}' is invalid ethereum address.";
}
