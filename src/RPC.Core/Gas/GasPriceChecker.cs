﻿using RPC.Core.Models;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using RPC.Core.Validation;
using FluentValidation;

namespace RPC.Core.Gas;

public class GasPriceChecker
{
    public uint MaxGweiGasPrice { get; private set; }
    public uint GasPrice { get; private set; }

    public GasPriceChecker(TransactionInput transactionInput, GasSettings gasSettings)
    {
        GasPrice = (uint)UnitConversion.Convert.FromWei(transactionInput.GasPrice, UnitConversion.EthUnit.Gwei);
        MaxGweiGasPrice = gasSettings.MaxGweiGasPrice;
        new GasPriceCheckerValidator().ValidateAndThrow(this);
    }
}