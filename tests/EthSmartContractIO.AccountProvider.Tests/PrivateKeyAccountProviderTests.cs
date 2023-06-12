using Moq;
using Xunit;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.AccountProvider.Tests;

public class PrivateKeyAccountProviderTests
{
    private const string privateKey = "0x1234";

    [Fact]
    internal void Ctor_InitializesAccountWithGivenPrivateKeyAndChainId()
    {
        var chainId = 1u;

        var account = new PrivateKeyAccountProvider(privateKey, chainId).Account;

        Assert.NotNull(account);
        Assert.Equal(privateKey, account.PrivateKey);
        Assert.NotNull(account.ChainId);
        Assert.Equal(chainId, account.ChainId.Value);
    }

    [Fact]
    internal void Ctor_InitializesAccountWithGivenSecretsProviderAndChainId()
    {
        var mockSecretsProvider = new Mock<ISecretsProvider>();
        mockSecretsProvider.Setup(x => x.Secret)
            .Returns(privateKey);
        var chainId = 1u;

        var account = new PrivateKeyAccountProvider(mockSecretsProvider.Object, chainId).Account;

        Assert.NotNull(account);
        Assert.Equal(privateKey, account.PrivateKey);
        Assert.NotNull(account.ChainId);
        Assert.Equal(chainId, account.ChainId.Value);
    }
}
