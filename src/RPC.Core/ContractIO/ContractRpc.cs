using RPC.Core.Types;
using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string Execute<TInput>(RpcAction<TInput> rpcAction, TInput actionInput) where TInput : IActionInput
    {
        if (!Enum.IsDefined(typeof(ActionType), actionInput.ActionType))
            throw new NotSupportedException($"Unsupported ActionType: {actionInput.ActionType}");

        return rpcAction.ExecuteAction(actionInput);
    }
}
