using RPC.Core.Managers;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Providers;

public class AccountProvider
{
    public Account Account { get; set; }
    public string AccountAddress { get; set; }
    
    public AccountProvider(IMnemonicProvider mnemonicProvider, int accountId, uint chainId)
    {
        var accountManager = new AccountManager(mnemonicProvider);
        Account = accountManager.GetAccount(accountId, new HexBigInteger(chainId));
        AccountAddress = Account.Address;
    }
}