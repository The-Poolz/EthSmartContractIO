using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Managers;

public class GasManager
{
    private readonly IWeb3 web3;
    public const int GasBufferFactor = 10;

    public GasManager(IWeb3 web3)
    {
        this.web3 = web3;
    }

    public TransactionInput EstimateGas(TransactionInput transaction)
    {
        var gasEstimate = web3.Eth.TransactionManager.EstimateGasAsync(transaction)
            .GetAwaiter()
            .GetResult();

        var bufferOfGasLimit = new HexBigInteger(gasEstimate.Value / GasBufferFactor);

        transaction.Gas = new HexBigInteger(gasEstimate.Value + bufferOfGasLimit.Value);

        return transaction;
    }

    public HexBigInteger GetCurrentWeiGasPrice() =>
        web3.Eth.GasPrice.SendRequestAsync()
            .GetAwaiter()
            .GetResult();
}
