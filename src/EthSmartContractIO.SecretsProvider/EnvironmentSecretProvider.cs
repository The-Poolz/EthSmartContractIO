using SecretsManager;
using EnvironmentManager;

namespace EthSmartContractIO.SecretsProvider;

public class EnvironmentSecretProvider : ISecretsProvider
{
    private static string SecretId =>
        EnvManager.GetEnvironmentValue<string>("SECRET_ID", raiseException: true);
    private static string SecretKey =>
        EnvManager.GetEnvironmentValue<string>("SECRET_KEY", raiseException: true);

    private readonly SecretManager secretManager;

    public EnvironmentSecretProvider(SecretManager? secretManager = null)
    {
        this.secretManager = secretManager ?? new SecretManager();
    }

    public virtual string Secret => secretManager.GetSecretValue(SecretId, SecretKey);
}
