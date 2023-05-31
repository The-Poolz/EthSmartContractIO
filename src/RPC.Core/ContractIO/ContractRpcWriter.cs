using RPC.Core.Gas;
using RPC.Core.Models;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using RPC.Core.Utility;
using RPC.Core.Managers;
using SecretsManager;
using Nethereum.Util;
using System.Numerics;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IContractIO
{
    private readonly Request request;
    private readonly GasPricer gasPricer;
    private readonly GasEstimator gasEstimator;
    private readonly TransactionSigner transactionSigner;
    private readonly TransactionSender transactionSender;

    public ContractRpcWriter(Request request, SecretManager secretManager)
    {
        var accountManager = new AccountManager(secretManager);
        var account = accountManager.GetAccount(request.AccountId, new HexBigInteger(request.ChainId));
        var web3 = Web3Base.CreateWeb3(request.RpcUrl, account);

        this.request = request;
        gasPricer = new(web3);
        gasEstimator = new(web3);
        transactionSigner = new(web3);
        transactionSender = new(web3);
    }

    private TransactionInput CreateActionInput() =>
        new(request.Data, request.To, request.Value)
        {
            ChainId = new HexBigInteger(request.ChainId),
            From = request.From
        };

    public string RunContractAction()
    {
        var transaction = gasEstimator.EstimateGas(CreateActionInput());
        transaction.GasPrice = gasPricer.GetCurrentWeiGasPrice();

        CheckGasLimits(transaction);

        var signedTransaction = transactionSigner.SignTransaction(transaction);
        return transactionSender.SendTransaction(signedTransaction);
    }

    public void CheckGasLimits(TransactionInput transactionInput)
    {
        if (request.GasSettings.MaxGasLimit > transactionInput.Gas.Value)
        {
            throw new InvalidOperationException("Gas limit exceeded.");
        }
        if (new BigInteger(UnitConversion.Convert.FromWei(request.GasSettings.MaxGweiGasPrice, UnitConversion.EthUnit.Gwei)) > transactionInput.GasPrice.Value)
        {
            throw new InvalidOperationException("Gas price exceeded.");
        }
    }
}
