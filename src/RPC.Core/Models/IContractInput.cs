using RPC.Core.Types;

namespace RPC.Core.Models;

public interface IActionInput
{
    ActionType ActionType { get; }
}
