using RPC.Core.Types;
using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x)
            .Must(RequireParameters);

        RuleFor(x => x)
            .Must(RequireWriteParameters);

        RuleFor(x => x)
            .Must(RequireReadParameters);
    }

    private bool RequireParameters(Request request)
    {
        if (!Enum.IsDefined(typeof(ActionType), request.ActionType))
            return false;

        if (request.ChainId == 0)
            return false;

        if (string.IsNullOrEmpty(request.To))
            return false;

        return true;
    }

    private bool RequireWriteParameters(Request request)
    {
        if (request.Web3 == null)
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
