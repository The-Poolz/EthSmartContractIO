using Moq;
using Xunit;
using SecretsManager;

namespace EthSmartContractIO.SecretsProvider.Tests;

public class EnvironmentSecretProviderTests
{
    [Fact]
    public void Constructor_WithDefaultParameters_CreatesInstance()
    {
        var mockSecretManager = new Mock<SecretManager>();

        var secretProvider = new EnvironmentSecretProvider(mockSecretManager.Object);

        Assert.NotNull(secretProvider);
        Assert.Equal(mockSecretManager.Object, GetPrivateFieldValue<SecretManager>(secretProvider));
    }

    [Fact]
    public void Constructor_WithNullSecretManager_CreatesInstanceWithDefaultSecretManager()
    {
        var secretProvider = new EnvironmentSecretProvider();

        Assert.NotNull(secretProvider);
        Assert.IsType<SecretManager>(GetPrivateFieldValue<SecretManager>(secretProvider));
    }

    [Fact]
    internal void Secret_WhenCalled_UsesSecretManagerGetSecretValue()
    {
        Environment.SetEnvironmentVariable("SECRET_ID", "secretId");
        Environment.SetEnvironmentVariable("SECRET_KEY", "secretKey");

        var mockSecretManager = new Mock<SecretManager>();
        mockSecretManager.Setup(x => x.GetSecretValue("secretId", "secretKey"))
            .Returns("secretValue");

        var secret = new EnvironmentSecretProvider(mockSecretManager.Object).Secret;

        Assert.Equal("secretValue", secret);
        mockSecretManager.Verify(x => x.GetSecretValue("secretId", "secretKey"), Times.Once());
    }

    private static T GetPrivateFieldValue<T>(object obj)
    {
        var fieldInfo = obj.GetType().GetField("secretManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (T)fieldInfo!.GetValue(obj)!;
    }
}
