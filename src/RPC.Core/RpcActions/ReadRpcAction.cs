using RPC.Core.Models;
using RPC.Core.ContractIO;

namespace RPC.Core.RpcActions;

public class ReadRpcAction : IRpcAction
{
    private readonly IContractIO contractRpcReader;

    public ReadRpcAction(IContractIO contractRpcReader)
    {
        this.contractRpcReader = contractRpcReader;
    }

    public virtual string ExecuteAction(Request request) =>
        contractRpcReader.RunContractAction();
}
