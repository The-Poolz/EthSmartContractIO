namespace EthSmartContractIO.Transaction;

public interface ITransactionSender
{
    public string SendTransaction(string signedTransaction);
}
