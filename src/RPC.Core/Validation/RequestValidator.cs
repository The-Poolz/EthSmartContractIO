using Nethereum.Util;
using RPC.Core.Types;
using RPC.Core.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace RPC.Core.Validation;

public class RequestValidator : AbstractValidator<Request>
{
    private const string MethodSignaturePattern = @"^0x[0-9a-fA-F]{8}";
    private const string ParametersPattern = @"([0-9a-fA-F]{64})*$";
    private static readonly Regex EthereumDataPattern = new($"{MethodSignaturePattern}{ParametersPattern}", default, TimeSpan.FromMinutes(1));

    public RequestValidator()
    {
        RuleFor(x => x.To)
            .NotNull()
            .NotEmpty()
            .Must(IsValidEthereumAddress)
            .WithMessage(GetInvalidAddressMessage("To"));

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
            .NotEmpty()
            .Must(IsValidEthereumData)
            .WithMessage("Parameter 'Data' not correctly formatted.");
    }

    private void RequireWriteParameters()
    {
        RuleFor(x => x.Web3)
            .NotNull();

        RuleFor(x => x.ChainId)
            .NotEqual(default(uint));

        RuleFor(x => x.From)
            .NotNull()
            .NotEmpty()
            .Must(IsValidEthereumAddress)
            .WithMessage(GetInvalidAddressMessage("From"));

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
            .WithMessage(GetNullMessage("Web3", "Read"));

        RuleFor(x => x.From)
            .Empty()
            .WithMessage(GetNullMessage("From", "Read"));

        RuleFor(x => x.Value)
            .Null()
            .WithMessage(GetNullMessage("Value", "Read"));

        RuleFor(x => x.GasSettings)
            .Null()
            .WithMessage(GetNullMessage("GasSettings", "Read"));
    }

    private void EnsureReadParametersAreNull()
    {
        RuleFor(x => x.RpcUrl)
            .Empty()
            .WithMessage(GetNullMessage("RpcUrl", "Write"));
    }

    private bool IsValidEthereumAddress(string address) =>
        AddressExtensions.IsValidEthereumAddressHexFormat(address);

    private bool IsValidEthereumData(string data) =>
        EthereumDataPattern.IsMatch(data);

    private static string GetInvalidAddressMessage(string fieldName) =>
        $"Parameter '{fieldName}' is invalid ethereum address.";

    private static string GetNullMessage(string fieldName, string operation) =>
        $"{fieldName} must be null for {operation} operations.";
}
