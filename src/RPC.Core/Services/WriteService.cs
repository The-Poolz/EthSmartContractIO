using RPC.Core.Gas;
using Nethereum.Web3;
using RPC.Core.Managers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Services;

public class WriteService
{
    private readonly GasPricer gasPricer;
    private readonly GasEstimator gasEstimator;
    private readonly TransactionManager transactionManager;

    public WriteService(IWeb3 web3)
    {
        gasPricer = new(web3);
        gasEstimator = new(web3);
        transactionManager = new TransactionManager(web3);
    }

    public string WriteToNetwork(TransactionInput transactionInput)
    {
        var transaction = gasEstimator.EstimateGas(transactionInput);
        transaction.GasPrice = gasPricer.GetCurrentWeiGasPrice();

        var signedTransaction = transactionManager.SignTransaction(transaction);
        return transactionManager.SendTransaction(signedTransaction);
    }

    public static IWeb3 CreateWeb3(string rpcConnection, Account account)
    {
        var client = new RpcClient(new Uri(rpcConnection));
        return new Web3(account, client);
    }
}
