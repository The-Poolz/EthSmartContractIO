using Nethereum.Web3.Accounts;

namespace EthSmartContractIO.Providers;

/// <summary>
/// Interface for providing account information.
/// </summary>
public interface IAccountProvider
{
    /// <summary>
    /// Gets the account associated with the provider.
    /// </summary>
    public Account Account { get; }
}
