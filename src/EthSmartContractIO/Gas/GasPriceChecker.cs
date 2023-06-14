using Nethereum.Util;
using FluentValidation;
using Nethereum.RPC.Eth.DTOs;
using EthSmartContractIO.Models;
using EthSmartContractIO.Gas.Validation;

namespace EthSmartContractIO.Gas;

/// <summary>
/// Class for checking the gas price of a transaction.
/// </summary>
public class GasPriceChecker
{
    public uint MaxGweiGasPrice { get; private set; }
    public uint GasPrice { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GasPriceChecker"/> class.
    /// </summary>
    /// <param name="transactionInput">The transaction to check the gas price of.</param>
    /// <param name="gasSettings">The settings for gas usage.</param>
    public GasPriceChecker(TransactionInput transactionInput, GasSettings gasSettings)
    {
        GasPrice = (uint)UnitConversion.Convert.FromWei(transactionInput.GasPrice, UnitConversion.EthUnit.Gwei);
        MaxGweiGasPrice = gasSettings.MaxGweiGasPrice;
        new GasPriceCheckerValidator().ValidateAndThrow(this);
    }
}
