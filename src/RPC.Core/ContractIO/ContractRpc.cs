using RPC.Core.Types;
using SecretsManager;
using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string ExecuteAction(Request request, SecretManager? secretManager = null)
    {
        secretManager ??= new SecretManager();

        var rpcAction = GetRpcAction(request, secretManager);

        return rpcAction.ExecuteAction(request);
    }

    private IRpcAction GetRpcAction(Request request, SecretManager secretManager) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request.RpcUrl) :
        new ContractRpcWriter(request.RpcUrl, request.AccountId, secretManager);
}
