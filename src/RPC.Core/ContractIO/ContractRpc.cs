﻿using RPC.Core.Types;
using RPC.Core.Models;
using FluentValidation;
using RPC.Core.Validation;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string ExecuteAction(Request request)
    {
        var validator = new RequestValidator();
        validator.ValidateAndThrow(request);

        var rpcAction = GetRpcAction(request);

        return rpcAction.ExecuteAction(request);
    }

    private RpcAction GetRpcAction(Request request) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request.RpcUrl!) :
        new ContractRpcWriter(request.Web3!);
}
