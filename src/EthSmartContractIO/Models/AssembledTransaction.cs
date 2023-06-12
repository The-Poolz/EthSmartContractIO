using EthSmartContractIO.ContractIO;
using EthSmartContractIO.Gas;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace EthSmartContractIO.Models;

public class AssembledTransaction : TransactionInput
{
    public AssembledTransaction(RpcRequest request, ServiceManager serviceManager) 
        : base(request.Data, request.To, request.WriteRequest!.Value)
    {
        ChainId = new HexBigInteger(serviceManager.Account.ChainId!.Value);
        From = serviceManager.Account.Address;
        Gas = new HexBigInteger(request.WriteRequest!.GasSettings.MaxGasLimit);
        GasPrice = serviceManager.GetRequiredService<IGasPricer>().GetCurrentWeiGasPrice();
    }
}
