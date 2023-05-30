using RPC.Core.Models;
using FluentValidation;

namespace RPC.Core.Validation;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x)
            .Must(RequireWriteParameters);
    }

    public bool RequireParameters(Request request)
    {
    }

    public bool RequireWriteParameters(Request request)
    {
    }

    public bool RequireReadParameters(Request request)
    {
    }
}
