using Nethereum.Util;
using System.Numerics;
using RPC.Core.Models;
using Nethereum.RPC.Eth.DTOs;
using RPC.Core.Gas.Exceptions;

namespace RPC.Core.Gas;

public class GasLimitChecker
{
    private readonly TransactionInput transactionInput;
    private readonly GasSettings gasSettings;

    public GasLimitChecker(TransactionInput transactionInput, GasSettings gasSettings)
    {
        this.transactionInput = transactionInput;
        this.gasSettings = gasSettings;
    }

    public GasLimitChecker CheckAndThrow() =>
        CheckGasLimit()
        .CheckGasPrice();

    private GasLimitChecker CheckGasLimit()
    {
        if (transactionInput.Gas.Value > gasSettings.MaxGasLimit)
        {
            throw new GasLimitExceededException();
        }
        return this;
    }

    private GasLimitChecker CheckGasPrice()
    {
        BigInteger maxWeiGasPrice = ConvertGweiToWei(gasSettings.MaxGweiGasPrice);
        if (transactionInput.GasPrice.Value > maxWeiGasPrice)
        {
            throw new GasPriceExceededException();
        }
        return this;
    }

    private static BigInteger ConvertGweiToWei(decimal gweiValue)
    {
        var etherValue = gweiValue * (decimal)Math.Pow(10, -9);
        return new UnitConversion().ToWei(etherValue);
    }
}
