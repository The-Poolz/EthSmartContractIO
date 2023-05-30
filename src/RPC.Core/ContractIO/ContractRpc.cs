namespace RPC.Core.ContractIO;

public class ContractRpc
{
    public virtual string Execute(IRpcAction rpcAction, object actionInput) =>
        rpcAction.ExecuteAction(actionInput);
}
