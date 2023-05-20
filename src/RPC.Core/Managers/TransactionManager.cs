using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Managers;

public class TransactionManager
{
    private readonly IWeb3 web3;

    public TransactionManager(IWeb3 web3)
    {
        this.web3 = web3;
    }

    public virtual string SignTransaction(TransactionInput transaction) =>
        web3.TransactionManager.Account.TransactionManager.SignTransactionAsync(transaction)
            .GetAwaiter()
            .GetResult();

    public virtual string SendTransaction(string signedTransaction) =>
        web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction)
            .GetAwaiter()
            .GetResult();
}
