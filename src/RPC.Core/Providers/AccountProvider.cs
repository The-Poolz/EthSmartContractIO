using SecretsManager;
using RPC.Core.Managers;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Providers;

public class AccountProvider
{
    public Account Account { get; set; }
    public string AccountAddress { get; set; }
    
    public AccountProvider(SecretManager secretManager, int accountId, uint chainId)
    {
        var accountManager = new AccountManager(secretManager);
        Account = accountManager.GetAccount(accountId, new HexBigInteger(chainId));
        AccountAddress = Account.Address;
    }
}