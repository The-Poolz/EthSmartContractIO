using RPC.Core.Types;
using SecretsManager;
using RPC.Core.Models;
using FluentValidation;
using RPC.Core.Validation;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string ExecuteAction(Request request, SecretManager? secretManager = null)
    {
        new RequestValidator().ValidateAndThrow(request);

        secretManager ??= new SecretManager();

        var rpcAction = GetRpcAction(request, secretManager);

        return rpcAction.ExecuteAction(request);
    }

    private IRpcAction GetRpcAction(Request request, SecretManager secretManager) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request.RpcUrl) :
        new ContractRpcWriter(request.RpcUrl, request.AccountId, secretManager);
}
