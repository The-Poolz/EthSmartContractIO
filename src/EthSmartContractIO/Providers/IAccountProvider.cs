using Nethereum.Web3.Accounts;

namespace EthSmartContractIO.Providers;

public interface IAccountProvider
{
    Account Account { get; }
}
