using RPC.Core.Providers;
using Nethereum.HdWallet;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Managers;

public class AccountManager
{
    private readonly Wallet wallet;

    public AccountManager(IMnemonicProvider mnemonicProvider)
    {
        wallet = WalletProvider.GetWallet(mnemonicProvider);
    }

    public Account GetAccount(int id, HexBigInteger chainId) =>
        wallet.GetAccount(id, chainId);
}
