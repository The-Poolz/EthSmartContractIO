using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Managers;

public class TransactionManager
{
    private readonly Web3 web3;

    public TransactionManager(Account account, RpcClient client)
        : this(new Web3(account, client))
    { }

    public TransactionManager(Web3 web3)
    {
        this.web3 = web3;
    }

    public string SignTransaction(TransactionInput transaction) =>
        web3.TransactionManager.Account.TransactionManager.SignTransactionAsync(transaction)
            .GetAwaiter()
            .GetResult();

    public string SendTransaction(string signedTransaction) =>
        web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction)
            .GetAwaiter()
            .GetResult();
}
