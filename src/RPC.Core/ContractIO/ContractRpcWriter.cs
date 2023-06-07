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
    private readonly IWeb3 web3;
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
        this.web3 = web3 ?? InitializeWeb3();
        this.gasEstimator = gasEstimator ?? new GasEstimator(this.web3);
        this.gasPricer = gasPricer ?? new GasPricer(this.web3);
        this.transactionSigner = transactionSigner ?? new TransactionSigner(this.web3);
        this.transactionSender = transactionSender ?? new TransactionSender(this.web3);
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
