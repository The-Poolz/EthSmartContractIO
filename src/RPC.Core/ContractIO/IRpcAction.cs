using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public interface IRpcAction
{
    public string ExecuteAction(Request request);
}
