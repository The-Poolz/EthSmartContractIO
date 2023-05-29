using RPC.Core.Types;

namespace RPC.Core.Models;

public class TransactionInput : Nethereum.RPC.Eth.DTOs.TransactionInput, IActionInput
{
    public ActionType ActionType { get => ActionType.Write; }
}
