using Nethereum.Web3;
using EthSmartContractIO.Utility;

namespace EthSmartContractIO.Transaction;

/// <summary>
/// Class for sending transactions.
/// </summary>
public class TransactionSender : Web3Base, ITransactionSender
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionSender"/> class.
    /// </summary>
    /// <param name="web3">The <see cref="Web3"/> instance to use.</param>
    public TransactionSender(IWeb3 web3) : base(web3) { }

    /// <summary>
    /// Sends a signed transaction.
    /// </summary>
    /// <param name="signedTransaction">The signed transaction to send.</param>
    /// <returns>The transaction hash.</returns>
    public virtual string SendTransaction(string signedTransaction) =>
        web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction)
            .GetAwaiter()
            .GetResult();
}
