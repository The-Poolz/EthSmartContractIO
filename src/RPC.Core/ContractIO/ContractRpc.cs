using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string Execute<TInput>(IRpcAction<TInput> rpcAction, TInput actionInput) where TInput : IActionInput =>
        rpcAction.ExecuteAction(actionInput);
}
