using SecretsManager;
using Nethereum.HdWallet;

namespace RPC.Core.Managers;

public static class WalletManager
{
    public static Wallet GetWallet(SecretManager secretManager) =>
        new(words: secretManager.GetSecretValue("Mnemonic", "string"), seedPassword: string.Empty);
}
