using RPC.Core.Gas;
using RPC.Core.Models;
using RPC.Core.Transaction;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Microsoft.Extensions.DependencyInjection;

namespace RPC.Core.ContractIO;

public class ContractRpcWriter : IContractIO
{
    private readonly RpcRequest request;
    private readonly IServiceProvider serviceRouterProvider;
    private IGasPricer GasPricer =>
        serviceRouterProvider.GetRequiredService<IGasPricer>();
    private ITransactionSigner TransactionSigner =>
        serviceRouterProvider.GetRequiredService<ITransactionSigner>();
    private ITransactionSender TransactionSender =>
        serviceRouterProvider.GetRequiredService<ITransactionSender>();

    public ContractRpcWriter(RpcRequest request, IServiceProvider? serviceProvider = null) 
    {
        serviceRouterProvider = new ServiceManager(request, serviceProvider);
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
