using RPC.Core.Gas;
using Nethereum.Web3;
using RPC.Core.Models;
using RPC.Core.Utility;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Microsoft.Extensions.DependencyInjection;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IContractIO
{
    private readonly RpcRequest request;
    private readonly IGasEstimator gasEstimator;
    private readonly IGasPricer gasPricer;
    private readonly ITransactionSigner transactionSigner;
    private readonly ITransactionSender transactionSender;

    public ContractRpcWriter(RpcRequest request, IServiceProvider? serviceProvider = null)
    {
        this.request = request;
        var web3 = serviceProvider?.GetService<IWeb3>() ?? InitializeWeb3();
        gasEstimator = serviceProvider?.GetService<IGasEstimator>() ?? new GasEstimator(web3);
        gasPricer = serviceProvider?.GetService<IGasPricer>() ?? new GasPricer(web3);
        transactionSigner = serviceProvider?.GetService<ITransactionSigner>() ?? new TransactionSigner(web3);
        transactionSender = serviceProvider?.GetService<ITransactionSender>() ?? new TransactionSender(web3);
    }

    public virtual string RunContractAction()
    {
        var transaction = gasEstimator.EstimateGas(CreateActionInput());
        transaction.GasPrice = gasPricer.GetCurrentWeiGasPrice();

        new GasLimitChecker(transaction, request.WriteRequest!.GasSettings).CheckAndThrow();

        var signedTransaction = transactionSigner.SignTransaction(transaction);
        return transactionSender.SendTransaction(signedTransaction);
    }

    public IWeb3 InitializeWeb3() =>
        Web3Base.CreateWeb3(request.RpcUrl, request.WriteRequest!.AccountProvider.Account);

    private TransactionInput CreateActionInput() =>
        new(request.Data, request.To, request.WriteRequest!.Value)
        {
            ChainId = new HexBigInteger(request.WriteRequest!.ChainId),
            From = request.WriteRequest!.AccountProvider.Account.Address
        };
}
