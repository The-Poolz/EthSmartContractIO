using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using EthSmartContractIO.Utility;

namespace EthSmartContractIO.Transaction;

/// <summary>
/// Class for signing transactions.
/// </summary>
public class TransactionSigner : Web3Base, ITransactionSigner
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionSigner"/> class.
    /// </summary>
    /// <param name="web3">The <see cref="Web3"/> instance to use.</param>
    public TransactionSigner(IWeb3 web3) : base(web3) { }

    /// <summary>
    /// Signs a transaction.
    /// </summary>
    /// <param name="transaction">The transaction to sign.</param>
    /// <returns>The signed transaction.</returns>
    public virtual string SignTransaction(TransactionInput transaction) =>
        web3.TransactionManager.Account.TransactionManager.SignTransactionAsync(transaction)
            .GetAwaiter()
            .GetResult();
}
