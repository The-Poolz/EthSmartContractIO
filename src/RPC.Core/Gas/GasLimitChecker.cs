using Nethereum.Util;
using System.Numerics;
using RPC.Core.Models;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Gas;

public static class GasLimitChecker
{
    public static void CheckGasLimits(TransactionInput transactionInput, GasSettings gasSettings)
    {
        CheckGasLimit(transactionInput, gasSettings);
        CheckGasPrice(transactionInput, gasSettings);
    }

    private static void CheckGasLimit(TransactionInput transactionInput, GasSettings gasSettings)
    {
        if (transactionInput.Gas.Value > gasSettings.MaxGasLimit)
        {
            throw new InvalidOperationException("Gas limit exceeded.");
        }
    }

    private static void CheckGasPrice(TransactionInput transactionInput, GasSettings gasSettings)
    {
        decimal etherValue = gasSettings.MaxGweiGasPrice * (decimal)Math.Pow(10, -9);
        BigInteger weiValue = new UnitConversion().ToWei(etherValue);
        if (transactionInput.GasPrice.Value > weiValue)
        {
            throw new InvalidOperationException("Gas price exceeded.");
        }
    }
}
