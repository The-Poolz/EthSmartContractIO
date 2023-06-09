using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using EthSmartContractIO.Utility;

namespace EthSmartContractIO.Transaction;

public class TransactionSigner : Web3Base, ITransactionSigner
{
    public TransactionSigner(IWeb3 web3) : base(web3) { }

    public virtual string SignTransaction(TransactionInput transaction) =>
        web3.TransactionManager.Account.TransactionManager.SignTransactionAsync(transaction)
            .GetAwaiter()
            .GetResult();
}
