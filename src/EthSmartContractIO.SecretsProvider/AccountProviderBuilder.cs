using EthSmartContractIO.AccountProvider;

namespace EthSmartContractIO.SecretsProvider;

public class AccountProviderBuilder
{
    private readonly ISecretsProvider secretsProvider;

    public AccountProviderBuilder(ISecretsProvider secretsProvider)
    {
        this.secretsProvider = secretsProvider;
    }

    public MnemonicAccountProvider BuildMnemonicAccountProvider(uint accountId, uint chainId, string seedPassword = "") =>
        new(secretsProvider.Secret, accountId, chainId, seedPassword);

    public PrivateKeyAccountProvider BuildPrivateKeyAccountProvider(uint chainId) =>
        new(secretsProvider.Secret, chainId);
}
