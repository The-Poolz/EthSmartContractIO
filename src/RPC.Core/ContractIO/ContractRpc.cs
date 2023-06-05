using RPC.Core.Types;
using RPC.Core.Models;
using RPC.Core.Providers;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    private readonly IMnemonicProvider mnemonicProvider;

    public ContractRpc(IMnemonicProvider mnemonicProvider)
    {
        this.mnemonicProvider = mnemonicProvider;
    }

    public virtual string ExecuteAction(RpcRequest request) =>
        GetContractIO(request).RunContractAction();

    private IContractIO GetContractIO(RpcRequest request) =>
        request.ActionType == ActionType.Read ?
        new ContractRpcReader(request) :
        new ContractRpcWriter(request, mnemonicProvider);
}
