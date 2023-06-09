using Nethereum.RPC.Eth.DTOs;

namespace EthSmartContractIO.Transaction;

public interface ITransactionSigner
{
    public string SignTransaction(TransactionInput transaction);
}
