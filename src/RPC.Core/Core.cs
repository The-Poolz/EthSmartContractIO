using FluentValidation;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using RPC.Core.ContractIO;
using RPC.Core.Models;
using RPC.Core.Types;
using RPC.Core.Validation;

namespace RPC.Core;

public class Core
{
    public string Execute(Request request)
    {
        var validator = GetActionValidator(request.ActionType);
        validator.ValidateAndThrow(request);

        var contractRpc = new ContractRpc();

        var rpcAction = GetRpcAction(request);
        var rpcInput = GetRpcInput(request);

        return contractRpc.Execute(rpcAction, rpcInput);
    }

    private AbstractValidator<Request> GetActionValidator(ActionType actionType) =>
        actionType switch
        {
            ActionType.Read => new ReadRequestValidator(),
            ActionType.Write => new WriteRequestValidator(),
            _ => throw new ArgumentException("Invalid ActionType"),
        };

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
        new(request.To, request.Data);
}
