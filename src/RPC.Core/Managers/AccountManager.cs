using SecretsManager;
using Nethereum.HdWallet;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Managers;

public class AccountManager
{
    private readonly Wallet wallet;

    public AccountManager(SecretManager secretManager)
    {
        wallet = WalletManager.GetWallet(secretManager);
    }

    public Account GetAccount(int id) => wallet.GetAccount(id);
}
