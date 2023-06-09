using Moq;
using Xunit;
using SecretsManager;

namespace EthSmartContractIO.SecretsProvider.Tests;

public class EnvironmentSecretProviderTests
{
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
}
