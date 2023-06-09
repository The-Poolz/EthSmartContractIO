using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    private readonly IServiceProvider? serviceProvider;

    public ContractRpc(IServiceProvider? serviceProvider = null)
    {
        this.serviceProvider = serviceProvider;
    }

    public virtual string ExecuteAction(RpcRequest request) =>
        GetContractIO(request).RunContractAction();

    private IContractIO GetContractIO(RpcRequest request) =>
        request.ActionIsRead ?
        new ContractRpcReader(request) :
        new ContractRpcWriter(
            request: request,
            serviceProvider: serviceProvider
        );
}
