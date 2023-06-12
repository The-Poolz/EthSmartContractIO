using Nethereum.HdWallet;
using Nethereum.Hex.HexTypes;

namespace EthSmartContractIO.Providers.Account;

public class MnemonicAccountProvider : IAccountProvider
{
    private readonly string mnemonicWords;
    private readonly uint accountId;
    private readonly uint chainId;
    private readonly string seedPassword = "";

    public MnemonicAccountProvider(params object[] args)
    {
        if (args.Length < 3)
            throw new ArgumentException("Three arguments required: mnemonic words, account ID and chain ID.", nameof(args));

        if (args[0] is string mw)
            mnemonicWords = mw;
        else
            throw new ArgumentException("The first argument must be a (string) mnemonic words.", nameof(args));

        if (args[1] is uint aid)
            accountId = aid;
        else
            throw new ArgumentException("The second argument must be a (uint) account ID.", nameof(args));

        if (args[2] is uint cid)
            chainId = cid;
        else
            throw new ArgumentException("The third argument must be a (uint) chain ID.", nameof(args));

        if (args.Length > 3)
        {
            if (args[3] is string sp)
                seedPassword = sp;
        }
    }

    public Nethereum.Web3.Accounts.Account GetAccount(params object[] args)
    {
        var wallet = new Wallet(words: mnemonicWords, seedPassword: seedPassword);
        return wallet.GetAccount((int)accountId, new HexBigInteger(chainId));
    }
}