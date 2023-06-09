using EthSmartContractIO.Gas;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace EthSmartContractIO.Models;

public class AssembledTransaction : TransactionInput
{
    public AssembledTransaction(RpcRequest request, IGasPricer gasPricer) 
        : base(request.Data, request.To, request.WriteRequest!.Value)
    {
        ChainId = new HexBigInteger(request.WriteRequest!.ChainId);
        From = request.WriteRequest!.AccountProvider.Account.Address;
        Gas = new HexBigInteger(request.WriteRequest!.GasSettings.MaxGasLimit);
        GasPrice = gasPricer.GetCurrentWeiGasPrice();
    }
}
