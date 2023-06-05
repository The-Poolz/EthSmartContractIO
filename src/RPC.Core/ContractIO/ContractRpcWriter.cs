using RPC.Core.Gas;
using Nethereum.Util;
using Nethereum.Web3;
using System.Numerics;
using RPC.Core.Models;
using RPC.Core.Utility;
using RPC.Core.Providers;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IContractIO
{
    private readonly RpcRequest request;
    private readonly IMnemonicProvider mnemonicProvider;
    private string accountAddress;

    public IWeb3? Web3 { get; set; }

    public ContractRpcWriter(RpcRequest request, IMnemonicProvider mnemonicProvider)
    {
        this.request = request;
        this.mnemonicProvider = mnemonicProvider;
    }

    public virtual string RunContractAction()
    {
        Web3 ??= InitializeWeb3();

        var transaction = new GasEstimator(Web3).EstimateGas(CreateActionInput());
        transaction.GasPrice = new GasPricer(Web3).GetCurrentWeiGasPrice();

        CheckGasLimits(transaction);

        var signedTransaction = new TransactionSigner(Web3).SignTransaction(transaction);
        return new TransactionSender(Web3).SendTransaction(signedTransaction);
    }

    public IWeb3 InitializeWeb3()
    {
        var accountProvider = new AccountProvider(mnemonicProvider, request.AccountId, request.ChainId);
        accountAddress = accountProvider.AccountAddress;
        return Web3Base.CreateWeb3(request.RpcUrl, accountProvider.Account);
    }

    private TransactionInput CreateActionInput() =>
        new(request.Data, request.To, request.Value)
        {
            ChainId = new HexBigInteger(request.ChainId),
            From = accountAddress
        };

    private void CheckGasLimits(TransactionInput transactionInput)
    {
        if (transactionInput.Gas.Value > request.GasSettings.MaxGasLimit)
        {
            throw new InvalidOperationException("Gas limit exceeded.");
        }

        decimal etherValue = request.GasSettings.MaxGweiGasPrice * (decimal)Math.Pow(10, -9);
        BigInteger weiValue = new UnitConversion().ToWei(etherValue);
        if (transactionInput.GasPrice.Value > weiValue)
        {
            throw new InvalidOperationException("Gas price exceeded.");
        }
    }
}
