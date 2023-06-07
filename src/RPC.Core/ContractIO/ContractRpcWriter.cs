using RPC.Core.Gas;
using Nethereum.Web3;
using RPC.Core.Models;
using RPC.Core.Utility;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IContractIO
{
    private readonly RpcRequest request;
    private readonly IGasEstimator gasEstimator;
    private readonly IGasPricer gasPricer;
    private readonly ITransactionSigner transactionSigner;
    private readonly ITransactionSender transactionSender;

    public ContractRpcWriter(
        RpcRequest request,
        IWeb3? web3 = null,
        IGasEstimator? gasEstimator = null,
        IGasPricer? gasPricer = null,
        ITransactionSigner? transactionSigner = null,
        ITransactionSender? transactionSender = null
    )
    {
        this.request = request;
        var web3Instance = web3 ?? InitializeWeb3();
        this.gasEstimator = gasEstimator ?? new GasEstimator(web3Instance);
        this.gasPricer = gasPricer ?? new GasPricer(web3Instance);
        this.transactionSigner = transactionSigner ?? new TransactionSigner(web3Instance);
        this.transactionSender = transactionSender ?? new TransactionSender(web3Instance);
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
