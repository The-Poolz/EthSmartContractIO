using Nethereum.Web3.Accounts;

namespace RPC.Core.Providers;

public interface IAccountProvider
{
    Account? Account { get; }
}
