using RPC.Core.Models;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public IServiceProvider? ServiceProvider { get; set; }

    public virtual string ExecuteAction(RpcRequest request) =>
        GetContractIO(request).RunContractAction();

    private IContractIO GetContractIO(RpcRequest request) =>
        request.ActionIsRead ?
        new ContractRpcReader(request) :
        new ContractRpcWriter(
            request: request,
            serviceProvider: ServiceProvider
        );
}
