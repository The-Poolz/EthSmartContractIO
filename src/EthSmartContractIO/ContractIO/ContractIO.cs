using EthSmartContractIO.Models;

namespace EthSmartContractIO.ContractIO;

public class ContractIO
{
    private readonly IServiceProvider? serviceProvider;

    public ContractIO(IServiceProvider? serviceProvider = null)
    {
        this.serviceProvider = serviceProvider;
    }

    public virtual string ExecuteAction(RpcRequest request) =>
        GetContractIO(request).RunContractAction();

    private IContractIO GetContractIO(RpcRequest request) =>
        request.ActionIsRead ?
        new ContractReader(request) :
        new ContractWriter(
            request: request,
            serviceProvider: serviceProvider
        );
}
