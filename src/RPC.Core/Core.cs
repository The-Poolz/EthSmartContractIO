using Flurl.Util;
using Nethereum.Hex.HexTypes;
using RPC.Core.ContractIO;
using RPC.Core.Models;
using RPC.Core.Types;

namespace RPC.Core;

public class Core
{
    public string Run(Request request)
    {
        var contractRpc = new ContractRpc();
        contractRpc.Execute();
    }

    private void SetupWrite(Request request)
    {
        var contractRpcWriter = new ContractRpcWriter(request.Web3);

        var transactionInput = new TransactionInput()
        {
            ChainId = new HexBigInteger(request.ChainId),
            To = request.To,
            From = request.From,
            Value = request.Value == null ? null : new HexBigInteger(request.Value.Value),
            Data = CreateData()
        };
        
    }

    private string? CreateData(Request request)
    {
        
    }
}
