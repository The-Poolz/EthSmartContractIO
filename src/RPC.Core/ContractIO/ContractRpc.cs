using RPC.Core.Types;
using RPC.Core.Models;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string Execute<TInput>(RpcAction<TInput> rpcAction, TInput actionInput) where TInput : IActionInput
    {
        return actionInput.ActionType switch
        {
            ActionType.Read => rpcAction.ExecuteAction(actionInput),
            ActionType.Write => rpcAction.ExecuteAction(actionInput),
            _ => throw new NotSupportedException($"Unsupported ActionType: {actionInput.ActionType}")
        };
    }
}
