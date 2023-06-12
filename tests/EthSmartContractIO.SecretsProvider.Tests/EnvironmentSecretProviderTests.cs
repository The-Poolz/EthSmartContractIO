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
        Assert.Equal(mockSecretManager.Object, GetPrivateFieldValue<SecretManager>(secretProvider, "secretManager"));
    }

    [Fact]
    public void Constructor_WithNullSecretManager_CreatesInstanceWithDefaultSecretManager()
    {
        var secretProvider = new EnvironmentSecretProvider();

        Assert.NotNull(secretProvider);
        Assert.IsType<SecretManager>(GetPrivateFieldValue<SecretManager>(secretProvider, "secretManager"));
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

    private static T GetPrivateFieldValue<T>(object obj, string fieldName)
    {
        var fieldInfo = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (T)fieldInfo.GetValue(obj);
    }
}
