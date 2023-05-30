using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.ActionType)
            .IsInEnum();

        RuleFor(x => x)
            .Must(RequireAnyRequest);
    }

    private bool RequireAnyRequest(Request request)
    {
        if (request.Write == null && request.Read == null)
            return false;

        return true;
    }

    private bool RequireWriteParameters(Request request)
    {
        if (request.Web3 == null)
            return false;

        if (string.IsNullOrEmpty(request.From))
            return false;

        if (request.GasSettings == null)
            return false;

        if (request.GasSettings.MaxGasLimit == 0)
            return false;

        if (request.GasSettings.MaxGweiGasPrice == 0)
            return false;

        if ((!string.IsNullOrEmpty(request.ContractNameWithVersion) || !string.IsNullOrEmpty(request.FunctionName)) &&
            (request.ContractNameWithVersion == null || request.FunctionName == null))
            return false;

        return true;
    }

    private bool RequireReadParameters(Request request)
    {
        if (string.IsNullOrEmpty(request.RpcUrl))
            return false;

        if ((!string.IsNullOrEmpty(request.ABI) || !string.IsNullOrEmpty(request.FunctionName)) &&
            (request.ABI == null || request.FunctionName == null))
            return false;

        return true;
    }
}
