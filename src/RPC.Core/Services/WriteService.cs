using Nethereum.Web3;
using RPC.Core.Managers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Services;

public class WriteService
{
    private readonly GasManager gasManager;
    private readonly TransactionManager transactionManager;

    public WriteService(IWeb3 web3)
    {
        gasManager = new GasManager(web3);
        transactionManager = new TransactionManager(web3);
    }

    public string WriteToNetwork(TransactionInput transactionInput)
    {
        var transaction = gasManager.EstimateGas(transactionInput);
        transaction.GasPrice = gasManager.GetCurrentWeiGasPrice();

        var signedTransaction = transactionManager.SignTransaction(transaction);
        return transactionManager.SendTransaction(signedTransaction);
    }

    public static IWeb3 CreateWeb3(string rpcConnection, Account account)
    {
        var client = new RpcClient(new Uri(rpcConnection));
        return new Web3(account, client);
    }
}
