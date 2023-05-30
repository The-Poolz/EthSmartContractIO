using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public abstract class RpcAction
{
    protected abstract string Execute(dynamic input);
    protected abstract dynamic CreateActionInput(Request request);

    public string ExecuteAction(Request request)
    {
        var input = CreateActionInput(request);
        return Execute(input);
    }
}
