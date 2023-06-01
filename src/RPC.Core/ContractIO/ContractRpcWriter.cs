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
using Nethereum.Web3;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IContractIO
{
    private IWeb3 web3;
    private readonly Request request;

    public ContractRpcWriter(Request request, SecretManager secretManager)
    {
        var accountManager = new AccountManager(secretManager);
        var account = accountManager.GetAccount(request.AccountId, new HexBigInteger(request.ChainId));
        web3 = Web3Base.CreateWeb3(request.RpcUrl, account);

        this.request = request;
    }

    private TransactionInput CreateActionInput() =>
        new(request.Data, request.To, request.Value)
        {
            ChainId = new HexBigInteger(request.ChainId),
            From = request.From
        };

    public virtual string RunContractAction()
    {
        var transaction = new GasEstimator(web3).EstimateGas(CreateActionInput());
        transaction.GasPrice = new GasPricer(web3).GetCurrentWeiGasPrice();

        CheckGasLimits(transaction);

        var signedTransaction = new TransactionSigner(web3).SignTransaction(transaction);
        return new TransactionSender(web3).SendTransaction(signedTransaction);
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

    public void SetWeb3(IWeb3 web3)
    {
        this.web3 = web3;
    }
}
