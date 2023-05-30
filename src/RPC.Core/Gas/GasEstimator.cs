using Nethereum.Web3;
using RPC.Core.Utility;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Gas;

public class GasEstimator : Web3Base
{
    public const int GasBufferFactor = 10;

    public GasEstimator(IWeb3 web3) : base(web3) { }

    public TransactionInput EstimateGas(TransactionInput transaction)
    {
        var gasEstimate = web3.Eth.TransactionManager.EstimateGasAsync(transaction)
            .GetAwaiter()
            .GetResult();

        var bufferOfGasLimit = new HexBigInteger(gasEstimate.Value / GasBufferFactor);

        transaction.Gas = new HexBigInteger(gasEstimate.Value + bufferOfGasLimit.Value);

        return transaction;
    }
}
