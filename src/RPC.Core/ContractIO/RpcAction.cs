using RPC.Core.Models;
using Newtonsoft.Json.Linq;

namespace RPC.Core.ContractIO;

public abstract class RpcAction<TInput> where TInput : IActionInput
{
    public abstract JToken ExecuteAction(TInput input);
}
