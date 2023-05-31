using RPC.Core.Gas;
using RPC.Core.Models;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using RPC.Core.Utility;
using RPC.Core.Managers;
using SecretsManager;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IRpcAction
{
    private readonly GasPricer gasPricer;
    private readonly GasEstimator gasEstimator;
    private readonly TransactionSigner transactionSigner;
    private readonly TransactionSender transactionSender;

    public ContractRpcWriter(string rpcUrl, int accountId, SecretManager secretManager)
    {
        var accountManager = new AccountManager(secretManager);
        var account = accountManager.GetAccount(accountId);
        var web3 = Web3Base.CreateWeb3(rpcUrl, account);

        gasPricer = new(web3);
        gasEstimator = new(web3);
        transactionSigner = new(web3);
        transactionSender = new(web3);
    }

    public string ExecuteAction(Request request)
    {
        var input = CreateActionInput(request);
        return WriteToNetwork(input);
    }

    private TransactionInput CreateActionInput(Request request) =>
        new(request.Data, request.To, request.Value)
        {
            ChainId = new HexBigInteger(request.ChainId),
            From = request.From
        };

    private string WriteToNetwork(TransactionInput transactionInput)
    {
        var transaction = gasEstimator.EstimateGas(transactionInput);
        transaction.GasPrice = gasPricer.GetCurrentWeiGasPrice();

        var signedTransaction = transactionSigner.SignTransaction(transaction);
        return transactionSender.SendTransaction(signedTransaction);
    }
}
