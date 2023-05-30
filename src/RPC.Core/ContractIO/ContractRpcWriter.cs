using RPC.Core.Gas;
using Nethereum.Web3;
using RPC.Core.Models;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : RpcAction
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

    protected override string Execute(dynamic input) =>
        WriteToNetwork(input);

    protected override dynamic CreateActionInput(Request request) =>
        new TransactionInput()
        {
            ChainId = new HexBigInteger(request.ChainId),
            To = request.To,
            From = request.From,
            Value = request.Value == null ? null : new HexBigInteger(request.Value.Value),
            Data = request.Data
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
