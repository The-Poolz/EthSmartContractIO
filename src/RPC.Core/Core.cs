using Flurl.Util;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using RPC.Core.ContractIO;
using RPC.Core.Managers;
using RPC.Core.Models;
using RPC.Core.Types;

namespace RPC.Core;

public class Core
{
    public string Run(Request request)
    {
        // validation

        var contractRpc = new ContractRpc();

        var rpcAction = GetRpcAction(request);
        var rpcInput = new object();

        return contractRpc.Execute(rpcAction, rpcInput);
    }

    private IRpcAction GetRpcAction(Request request) =>
        request.ActionType switch
        {
            ActionType.Read => new ContractRpcReader(request.RpcUrl!),
            ActionType.Write => new ContractRpcWriter(request.Web3!),
            _ => throw new ArgumentException("Invalid ActionType"),
        };

    private object GetRpcInput(Request request) =>
        request.ActionType switch
        {
            ActionType.Read => CreateTransactionInput(request),
            ActionType.Write => CreateRpcRequest(request),
            _ => throw new ArgumentException("Invalid ActionType"),
        };

    private TransactionInput CreateTransactionInput(Request request)
    {
        var contractManager = new ContractManager(request.Web3!, request.ABI, request.To);
        var function = contractManager.GetMethod(request.FunctionName);
        var data = function.CreateTransactionInput(request.To, request.Params).Data;

        var transactionInput = new TransactionInput()
        {
            ChainId = new HexBigInteger(request.ChainId),
            To = request.To,
            From = request.From,
            Value = request.Value == null ? null : new HexBigInteger(request.Value.Value),
            Data = data
        };

        return transactionInput;
    }

    private RpcRequest CreateRpcRequest(Request request)
    {

    }
}
