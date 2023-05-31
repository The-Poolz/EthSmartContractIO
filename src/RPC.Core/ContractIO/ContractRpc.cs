using RPC.Core.Types;
using SecretsManager;
using RPC.Core.Models;
using RPC.Core.RpcActions;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string ExecuteAction(Request request, SecretManager? secretManager = null)
    {
        secretManager ??= new SecretManager();

        var contractIO = GetContractIO(request, secretManager);

        var rpcAction = GetRpcAction(contractIO, request.ActionType);

        return rpcAction.ExecuteAction(request);
    }

    public virtual IRpcAction GetRpcAction(IContractIO contractIO, ActionType actionType) =>
        actionType == ActionType.Read ?
        new ReadRpcAction(contractIO) :
        new WriteRpcAction(contractIO);

    public virtual IContractIO GetContractIO(Request request, SecretManager secretManager) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request) :
        new ContractRpcWriter(request, secretManager);
}
