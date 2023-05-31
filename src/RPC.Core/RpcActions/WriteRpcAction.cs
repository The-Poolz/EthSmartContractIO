using RPC.Core.Models;
using RPC.Core.ContractIO;

namespace RPC.Core.RpcActions;

public class WriteRpcAction : IRpcAction
{
    private readonly IContractIO contractRpcWriter;

    public WriteRpcAction(IContractIO contractRpcWriter)
    {
        this.contractRpcWriter = contractRpcWriter;
    }

    public virtual string ExecuteAction(Request request) =>
        contractRpcWriter.RunContractAction();
}
