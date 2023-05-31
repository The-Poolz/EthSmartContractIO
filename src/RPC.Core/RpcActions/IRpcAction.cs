using RPC.Core.Models;

namespace RPC.Core.RpcActions;

public interface IRpcAction
{
    public string ExecuteAction(Request request);
}
