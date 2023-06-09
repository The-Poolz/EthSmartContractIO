﻿using FluentValidation;
using RPC.Core.Gas;

namespace RPC.Core.Validation;

public class GasPriceCheckerValidator : AbstractValidator<GasPriceChecker>
{
    public GasPriceCheckerValidator()
    {
        RuleFor(x => x.MaxGweiGasPrice)
            .GreaterThanOrEqualTo(x => x.GasPrice)
            .WithMessage("Gas price is too high.");
    }
}