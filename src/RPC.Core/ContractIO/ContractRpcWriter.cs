using RPC.Core.Gas;
using Nethereum.Web3;
using RPC.Core.Models;
using RPC.Core.Utility;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Microsoft.Extensions.DependencyInjection;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : Web3Base, IContractIO
{
    private readonly RpcRequest request;
    private IGasPricer GasPricer => serviceProvider?.GetService<IGasPricer>() ?? new GasPricer(web3);
    private ITransactionSigner TransactionSigner =>
        serviceProvider?.GetService<ITransactionSigner>() ?? new TransactionSigner(web3);
    private ITransactionSender TransactionSender =>
        serviceProvider?.GetService<ITransactionSender>() ?? new TransactionSender(web3);
    private readonly IServiceProvider? serviceProvider;

    public ContractRpcWriter(RpcRequest request, IServiceProvider? serviceProvider = null) 
        : base(serviceProvider?.GetService<IWeb3>() ??
            CreateWeb3(request.RpcUrl, request.WriteRequest!.AccountProvider.Account))
    {
        this.serviceProvider = serviceProvider;
        this.request = request;
    }

    public virtual string RunContractAction() =>
        TransactionSender.SendTransaction(
            TransactionSigner.SignTransaction(
                CreateActionInput));

    private TransactionInput CreateActionInput =>
        new(request.Data, request.To, request.WriteRequest!.Value)
        {
            ChainId = new HexBigInteger(request.WriteRequest!.ChainId),
            From = request.WriteRequest!.AccountProvider.Account.Address,
            Gas = new HexBigInteger(request.WriteRequest!.GasSettings.MaxGasLimit),
            GasPrice = GasPricer.GetCurrentWeiGasPrice()
        };
}
