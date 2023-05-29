using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public abstract class RpcAction<TInput> where TInput : IActionInput
{
    public abstract string ExecuteAction(TInput input);
}
