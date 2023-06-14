namespace EthSmartContractIO.Transaction;

/// <summary>
/// Interface for sending transactions.
/// </summary>
public interface ITransactionSender
{
    /// <summary>
    /// Sends a signed transaction.
    /// </summary>
    /// <param name="signedTransaction">The signed transaction to send.</param>
    /// <returns>The transaction hash.</returns>
    public string SendTransaction(string signedTransaction);
}
