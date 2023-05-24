using SecretsManager;
using Nethereum.HdWallet;
using EnvironmentManager;

namespace RPC.Core.Managers;

public static class WalletManager
{
    private static string SecretId =>
        EnvManager.GetEnvironmentValue<string>("SECRET_MNEMONIC_ID");
    private static string SecretKey =>
        EnvManager.GetEnvironmentValue<string>("SECRET_MNEMONIC_KEY");

    public static Wallet GetWallet(SecretManager secretManager) =>
        new(words: secretManager.GetSecretValue(SecretId, SecretKey), seedPassword: string.Empty);
}
