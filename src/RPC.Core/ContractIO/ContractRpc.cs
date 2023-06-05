using RPC.Core.Types;
using RPC.Core.Models;
using RPC.Core.Providers;

namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string ExecuteAction(RpcRequest request) =>
        GetRpcReader(request).RunContractAction();
    public virtual string ExecuteAction(RpcRequest request, IMnemonicProvider mnemonicProvider) =>
        GetRpcWriter(request, mnemonicProvider).RunContractAction();

    private static ContractRpcReader GetRpcReader(RpcRequest request) => new(request);
    private static ContractRpcWriter GetRpcWriter(RpcRequest request) => new(request);
}
