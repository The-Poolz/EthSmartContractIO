using Nethereum.HdWallet;

namespace RPC.Core.Providers;

public static class WalletProvider
{
    public static Wallet GetWallet(IMnemonicProvider mnemonicProvider) =>
        new(words: mnemonicProvider.GetMnemonic(), seedPassword: string.Empty);
}
