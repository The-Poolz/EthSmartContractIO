using SecretsManager;
using EnvironmentManager;

namespace SmartContractIO.SecretsProvider;

public class EnvironmentSecretProvider : ISecretsProvider
{
    private static string SecretId =>
        EnvManager.GetEnvironmentValue<string>("SECRET_ID", raiseException: true);
    private static string SecretKey =>
        EnvManager.GetEnvironmentValue<string>("SECRET_KEY", raiseException: true);

    private readonly SecretManager secretManager;

    public EnvironmentSecretProvider(SecretManager secretManager)
    {
        this.secretManager = secretManager;
    }

    public virtual string Secret => secretManager.GetSecretValue(SecretId, SecretKey);
}
