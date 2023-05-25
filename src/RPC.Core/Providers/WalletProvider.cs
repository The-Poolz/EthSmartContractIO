using SecretsManager;
using EnvironmentManager;
using Nethereum.HdWallet;

namespace RPC.Core.Providers;

public static class WalletProvider
{
    private static string SecretId =>
        EnvManager.GetEnvironmentValue<string>("SECRET_MNEMONIC_ID", raiseException: true);
    private static string SecretKey =>
        EnvManager.GetEnvironmentValue<string>("SECRET_MNEMONIC_KEY", raiseException: true);

    public static Wallet GetWallet(SecretManager secretManager) =>
        new(words: secretManager.GetSecretValue(SecretId, SecretKey), seedPassword: string.Empty);
}
