using Nethereum.Util;
using RPC.Core.Models;
using FluentValidation;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace RPC.Core.Validation;

internal class RpcRequestValidator : AbstractValidator<RpcRequest>
{
    private const string MethodSignaturePattern = @"^0x[0-9a-fA-F]{8}";
    private const string ParametersPattern = @"([0-9a-fA-F]{64})*$";
    private static readonly Regex EthereumDataPattern = new($"{MethodSignaturePattern}{ParametersPattern}", default, TimeSpan.FromMinutes(1));

    public RpcRequestValidator()
    {
        RuleFor(x => x.Params[0]["to"])
            .NotNull()
            .WithMessage("Parameter 'to' is null.")
            .Must(IsValidEthereumAddress)
            .WithMessage("Parameter 'to' is empty or not correctly formatted.");
        RuleFor(x => x.Params[0]["data"])
            .NotNull()
            .WithMessage("Parameter 'data' is null.")
            .Must(IsValidEthereumData)
            .WithMessage("Parameter 'data' is empty or not correctly formatted.");
    }

    private bool IsValidEthereumAddress(JToken? data)
    {
        var ethereumAddress = data?.ToString() ?? string.Empty;
        return AddressExtensions.IsValidEthereumAddressHexFormat(ethereumAddress);
    }
    private bool IsValidEthereumData(JToken? data)
    {
        var ethereumData = data?.ToString() ?? string.Empty;
        return EthereumDataPattern.IsMatch(ethereumData);
    }
}
