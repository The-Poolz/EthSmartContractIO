using RPC.Core.Providers;
using Nethereum.HdWallet;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace SmartContractIO.AccountProvider;

public class MnemonicAccountProvider : IAccountProvider
{
    public Account? Account { get; protected set; }

    public MnemonicAccountProvider() { }

    public MnemonicAccountProvider(string mnemonicWords, uint accountId, uint chainId, string seedPassword = "")
    {
        SetAccount(mnemonicWords, accountId, chainId, seedPassword);
    }

    public void SetAccount(string mnemonicWords, uint accountId, uint chainId, string seedPassword = "")
    {
        var wallet = new Wallet(words: mnemonicWords, seedPassword: seedPassword);
        Account = wallet.GetAccount((int)accountId, new HexBigInteger(chainId));
    }
}