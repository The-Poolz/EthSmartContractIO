using Nethereum.Web3;
using RPC.Core.Utility;

namespace RPC.Core.Transaction;

public class TransactionSender : Web3Base
{
    public TransactionSender(IWeb3 web3) : base(web3) { }

    public virtual string SendTransaction(string signedTransaction) =>
        web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction)
            .GetAwaiter()
            .GetResult();
}
