using Nethereum.Web3;
using RPC.Core.Managers;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.Services;

public class WriteService
{
    private readonly ContractManager contractManager;
    private readonly TransactionManager transactionManager;

    public WriteService(IWeb3 web3, string contractABI, string contractAddress)
    {
        contractManager = new ContractManager(web3, contractABI, contractAddress);
        transactionManager = new TransactionManager(web3);
    }

    public string WriteToNetwork(
        HexBigInteger chainId,
        string accountAddress,
        string contractAddress,
        HexBigInteger gasLimit,
        HexBigInteger gasPriceGwei,
        string methodName,
        params object[] functionInput
    )
    {
        var function = contractManager.GetMethod(methodName);
        var transaction = TransactionCreatorManager.CreateTransactionInput(chainId, accountAddress, contractAddress, gasLimit, gasPriceGwei, function, functionInput);
        var signedTransaction = transactionManager.SignTransaction(transaction);
        return transactionManager.SendTransaction(signedTransaction);
    }

    public static IWeb3 CreateWeb3(string rpcConnection, Account account)
    {
        var client = new RpcClient(new Uri(rpcConnection));
        return new Web3(account, client);
    }
}
