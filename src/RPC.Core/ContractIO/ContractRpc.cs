using RPC.Core.Types;
using RPC.Core.Models;
using RPC.Core.RpcActions;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string ExecuteAction(Request request)
    {
        var contractIO = GetContractIO(request);

        var rpcAction = GetRpcAction(contractIO, request.ActionType);

        return rpcAction.ExecuteAction(request);
    }

    private IRpcAction GetRpcAction(IContractIO contractIO, ActionType actionType) =>
        actionType == ActionType.Read ?
        new ReadRpcAction(contractIO) :
        new WriteRpcAction(contractIO);

    private IContractIO GetContractIO(Request request) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request) :
        new ContractRpcWriter(request);
}
