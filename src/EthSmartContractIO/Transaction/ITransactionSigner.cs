using Nethereum.RPC.Eth.DTOs;

namespace EthSmartContractIO.Transaction;

/// <summary>
/// Interface for signing transactions.
/// </summary>
public interface ITransactionSigner
{
    /// <summary>
    /// Signs a transaction.
    /// </summary>
    /// <param name="transaction">The transaction to sign.</param>
    /// <returns>The signed transaction.</returns>
    public string SignTransaction(TransactionInput transaction);
}
