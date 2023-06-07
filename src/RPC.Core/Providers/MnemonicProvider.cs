using Nethereum.HdWallet;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Providers;

public class MnemonicProvider : IAccountProvider
{
    public Account Account { get; private set; }

    public MnemonicProvider(string mnemonicWords, uint accountId, uint chainId, string seedPassword = "")
    {
        var wallet = new Wallet(words: mnemonicWords, seedPassword: seedPassword);
        Account = wallet.GetAccount((int)accountId, new HexBigInteger(chainId));
    }
}
