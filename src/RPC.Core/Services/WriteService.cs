using Nethereum.Web3;
using RPC.Core.Managers;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Services;

public class WriteService
{
    private readonly GasManager gasManager;
    private readonly TransactionSigner transactionSigner;
    private readonly TransactionSender transactionSender;

    public WriteService(IWeb3 web3)
    {
        gasManager = new(web3);
        transactionSigner = new(web3);
        transactionSender = new(web3);
    }

    public string WriteToNetwork(TransactionInput transactionInput)
    {
        var transaction = gasManager.EstimateGas(transactionInput);
        transaction.GasPrice = gasManager.GetCurrentWeiGasPrice();

        var signedTransaction = transactionSigner.SignTransaction(transaction);
        return transactionSender.SendTransaction(signedTransaction);
    }

    public static IWeb3 CreateWeb3(string rpcConnection, Account account)
    {
        var client = new RpcClient(new Uri(rpcConnection));
        return new Web3(account, client);
    }
}
