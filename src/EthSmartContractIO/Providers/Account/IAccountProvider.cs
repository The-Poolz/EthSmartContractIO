namespace EthSmartContractIO.Providers.Account;

public interface IAccountProvider
{
    Nethereum.Web3.Accounts.Account GetAccount(params object[] args);
}
