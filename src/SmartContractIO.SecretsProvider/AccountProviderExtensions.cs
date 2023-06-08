using SmartContractIO.AccountProvider;

namespace SmartContractIO.SecretsProvider;

public static class AccountProviderExtensions
{
    public static MnemonicAccountProvider UsingSecret(this MnemonicAccountProvider provider, ISecretsProvider secretsProvider, uint accountId, uint chainId, string seedPassword = "")
    {
        provider.SetAccount(secretsProvider.Secret, accountId, chainId, seedPassword);
        return provider;
    }

    public static PrivateKeyAccountProvider UsingSecret(this PrivateKeyAccountProvider provider, ISecretsProvider secretsProvider, uint chainId)
    {
        provider.SetAccount(secretsProvider.Secret, chainId);
        return provider;
    }
}
