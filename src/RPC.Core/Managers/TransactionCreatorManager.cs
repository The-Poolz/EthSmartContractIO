using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Managers;

public class TransactionCreatorManager
{
    public virtual TransactionInput CreateTransactionInput(
        HexBigInteger chainId,
        string accountAddress,
        string contractAddress,
        HexBigInteger gasLimit,
        HexBigInteger gasPriceGwei,
        Function function,
        params object[] functionInput
    )
    {
        var transaction = function.CreateTransactionInput(accountAddress, functionInput);
        transaction.GasPrice = gasPriceGwei;
        transaction.Gas = gasLimit;
        transaction.ChainId = chainId;
        transaction.To = contractAddress;

        return transaction;
    }
}
