using RPC.Core.Gas;
using Nethereum.Web3;
using RPC.Core.Models;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IRpcAction
{
    private readonly GasPricer gasPricer;
    private readonly GasEstimator gasEstimator;
    private readonly TransactionSigner transactionSigner;
    private readonly TransactionSender transactionSender;

    public ContractRpcWriter(IWeb3 web3)
    {
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

    // This method need to be in other place
    public static IWeb3 CreateWeb3(string rpcConnection, Account account)
    {
        var client = new RpcClient(new Uri(rpcConnection));
        return new Web3(account, client);
    }
}
