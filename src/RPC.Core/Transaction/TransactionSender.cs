using Nethereum.Web3;

namespace RPC.Core.Transaction;

public class TransactionSender : TransactionBase
{
    public TransactionSender(IWeb3 web3) : base(web3) { }

    public virtual string SendTransaction(string signedTransaction) =>
        web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction)
            .GetAwaiter()
            .GetResult();
}
