namespace EthSmartContractIO.Providers;

/// <summary>
/// Interface for providing secret information.
/// </summary>
public interface ISecretsProvider
{
    /// <summary>
    /// Gets the secret associated with the provider.
    /// </summary>
    public string Secret { get; }
}