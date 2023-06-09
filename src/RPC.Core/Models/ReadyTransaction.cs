using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using RPC.Core.Gas;

namespace RPC.Core.Models;

public class Transaction : TransactionInput
{
    public Transaction(RpcRequest request, GasPricer gasPricer) 
        : base(request.Data, request.To, request.WriteRequest!.Value)
    {
        ChainId = new HexBigInteger(request.WriteRequest!.ChainId);
        From = request.WriteRequest!.AccountProvider.Account.Address;
        Gas = new HexBigInteger(request.WriteRequest!.GasSettings.MaxGasLimit);
        GasPrice = gasPricer.GetCurrentWeiGasPrice();
    }
}
