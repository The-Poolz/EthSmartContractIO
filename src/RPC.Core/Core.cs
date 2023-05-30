using RPC.Core.Types;
using FluentValidation;
using RPC.Core.Models;
using RPC.Core.ContractIO;
using RPC.Core.Validation;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

namespace RPC.Core;

public class Core
{
    public string Execute(Request request)
    {
        var validator = new RequestValidator();
        validator.ValidateAndThrow(request);

        var contractRpc = new ContractRpc();

        var rpcAction = GetRpcAction(request);
        var rpcInput = GetRpcInput(request);

        return contractRpc.Execute(rpcAction, rpcInput);
    }

    private IRpcAction GetRpcAction(Request request) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request.RpcUrl!) :
        new ContractRpcWriter(request.Web3!);

    private object GetRpcInput(Request request) =>
        request.ActionType == ActionType.Read ?
        CreateRpcRequest(request) :
        CreateTransactionInput(request);

    private TransactionInput CreateTransactionInput(Request request) =>
        new()
        {
            ChainId = new HexBigInteger(request.ChainId),
            To = request.To,
            From = request.From,
            Value = request.Value == null ? null : new HexBigInteger(request.Value.Value),
            Data = request.Data
        };

    private RpcRequest CreateRpcRequest(Request request) =>
        new(request.To, request.Data!);
}
