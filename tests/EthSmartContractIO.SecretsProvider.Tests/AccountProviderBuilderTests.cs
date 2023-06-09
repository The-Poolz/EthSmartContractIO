using Moq;
using Xunit;

namespace EthSmartContractIO.SecretsProvider.Tests;

public class AccountProviderBuilderTests
{
    private readonly Mock<ISecretsProvider> mockSecretsProvider = new();

    [Fact]
    internal void BuildMnemonicAccountProvider_WhenCalled_UsesSecretsProviderSecret()
    {
        uint accountId = 1;
        uint chainId = 2;
        mockSecretsProvider.Setup(x => x.Secret)
            .Returns("inhale often sunny friend liquid lobster health snap season actor team waste knife glue width");
        var accountProviderBuilder = new AccountProviderBuilder(mockSecretsProvider.Object);

        var accountProvider = accountProviderBuilder.BuildMnemonicAccountProvider(accountId, chainId);

        Assert.Equal(chainId, accountProvider.Account.ChainId);
    }

    [Fact]
    internal void BuildPrivateKeyAccountProvider_WhenCalled_UsesSecretsProviderSecret()
    {
        uint chainId = 2;
        mockSecretsProvider.Setup(x => x.Secret)
            .Returns("0x4e3c79ee2f53da4e456cb13887f4a7d59488677e9e48b6fb6701832df828f7e9");
        var accountProviderBuilder = new AccountProviderBuilder(mockSecretsProvider.Object);

        var accountProvider = accountProviderBuilder.BuildPrivateKeyAccountProvider(chainId);

        Assert.Equal(chainId, accountProvider.Account.ChainId);
    }
}