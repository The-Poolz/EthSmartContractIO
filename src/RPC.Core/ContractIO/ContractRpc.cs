using RPC.Core.Types;
using Nethereum.Web3;
using RPC.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public IServiceProvider? ServiceProvider { get; set; }

    public virtual string ExecuteAction(RpcRequest request) =>
        GetContractIO(request).RunContractAction();

    private IContractIO GetContractIO(RpcRequest request) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request) :
        new ContractRpcWriter(request, web3: ServiceProvider?.GetService<IWeb3>());
}
