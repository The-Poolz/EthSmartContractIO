using RPC.Core.Types;

namespace RPC.Core.Models;

public class RpcRequestWithActionType : RpcRequest, IActionInput
{
    public RpcRequestWithActionType(string to, string data) : base(to, data) { }
    public ActionType ActionType { get => ActionType.Read; }
}
