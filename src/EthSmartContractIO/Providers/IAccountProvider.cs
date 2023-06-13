using Nethereum.Web3.Accounts;

namespace EthSmartContractIO.Providers;

public interface IAccountProvider
{
    public Account Account { get; }
}
