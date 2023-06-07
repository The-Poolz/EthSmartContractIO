using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Transaction;

public interface ITransactionSigner
{
    public string SignTransaction(TransactionInput transaction);
}
