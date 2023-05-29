using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public interface IRpcAction<TInput> where TInput : IActionInput
{
    public string ExecuteAction(TInput input);
}
