using Nethereum.Util;
using System.Numerics;
using RPC.Core.Models;
using Nethereum.RPC.Eth.DTOs;

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

    public void CheckGasLimits()
    {
        CheckGasLimit();
        CheckGasPrice();
    }

    private void CheckGasLimit()
    {
        if (transactionInput.Gas.Value > gasSettings.MaxGasLimit)
        {
            throw new InvalidOperationException("Gas limit exceeded.");
        }
    }

    private void CheckGasPrice()
    {
        decimal etherValue = gasSettings.MaxGweiGasPrice * (decimal)Math.Pow(10, -9);
        BigInteger weiValue = new UnitConversion().ToWei(etherValue);
        if (transactionInput.GasPrice.Value > weiValue)
        {
            throw new InvalidOperationException("Gas price exceeded.");
        }
    }
}
