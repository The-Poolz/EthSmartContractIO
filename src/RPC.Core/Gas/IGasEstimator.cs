using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Gas;

public interface IGasEstimator
{
    public TransactionInput EstimateGas(TransactionInput transaction);
}
