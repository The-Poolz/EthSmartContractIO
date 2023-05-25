using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Transaction;

public class TransactionSigner : TransactionBase
{
    public TransactionSigner(IWeb3 web3) : base(web3) { }

    public virtual string SignTransaction(TransactionInput transaction) =>
        web3.TransactionManager.Account.TransactionManager.SignTransactionAsync(transaction)
            .GetAwaiter()
            .GetResult();
}
