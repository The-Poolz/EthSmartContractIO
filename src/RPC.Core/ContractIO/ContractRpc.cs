using RPC.Core.Types;
using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string ExecuteAction(RpcRequest request)
    {
        var contractIO = GetContractIO(request);

        return contractIO.RunContractAction();
    }

    private static IContractIO GetContractIO(RpcRequest request) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request) :
        new ContractRpcWriter(request);
}
