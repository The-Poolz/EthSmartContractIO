using Nethereum.Util;
using FluentValidation;
using Nethereum.RPC.Eth.DTOs;
using EthSmartContractIO.Models;
using EthSmartContractIO.Validation;

namespace EthSmartContractIO.Gas;

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
