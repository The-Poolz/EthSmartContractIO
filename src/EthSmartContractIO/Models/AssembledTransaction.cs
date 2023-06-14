using EthSmartContractIO.Gas;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace EthSmartContractIO.Models;

/// <summary>
/// Class for assembling a transaction.
/// </summary>
public class AssembledTransaction : TransactionInput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssembledTransaction"/> class.
    /// </summary>
    /// <param name="request">The <see cref="RpcRequest"/> to execute.</param>
    /// <param name="gasPricer">The gas pricer to use.</param>
    public AssembledTransaction(RpcRequest request, IGasPricer gasPricer) 
        : base(request.Data, request.To, request.WriteRequest!.Value)
    {
        ChainId = new HexBigInteger(request.WriteRequest!.ChainId);
        From = request.WriteRequest!.AccountProvider.Account.Address;
        Gas = new HexBigInteger(request.WriteRequest!.GasSettings.MaxGasLimit);
        GasPrice = gasPricer.GetCurrentWeiGasPrice();
    }
}
