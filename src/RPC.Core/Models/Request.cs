using RPC.Core.Types;

namespace RPC.Core.Models;

public class Request
{
    public ActionType ActionType { get; set; }
    public WriteRequest? Write { get; set; }
    public ReadRequest? Read { get; set; }
}
