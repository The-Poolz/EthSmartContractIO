using Moq;
using Xunit;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.AccountProvider.Tests;

public class MnemonicAccountProviderTests
{
    private const string mnemonicWords = "abandon amount liar amount expire adjust cage candy arch gather drum buyer";

    [Fact]
    internal void Ctor_InitializesAccountWithGivenMnemonicWordsAccountIdAndChainId()
    {
        var accountId = 0u;
        var chainId = 1u;

        var account = new MnemonicAccountProvider(mnemonicWords, accountId, chainId).Account;

        Assert.NotNull(account);
        Assert.NotNull(account.PrivateKey);
        Assert.NotNull(account.ChainId);
        Assert.Equal(chainId, account.ChainId.Value);
    }

    [Fact]
    internal void Ctor_InitializesAccountWithGivenSecretsProviderAccountIdAndChainId()
    {
        var mockSecretsProvider = new Mock<ISecretsProvider>();
        mockSecretsProvider.Setup(x => x.Secret)
            .Returns(mnemonicWords);
        var accountId = 0u;
        var chainId = 1u;

        var account = new MnemonicAccountProvider(mockSecretsProvider.Object, accountId, chainId).Account;

        Assert.NotNull(account);
        Assert.NotNull(account.PrivateKey);
        Assert.NotNull(account.ChainId);
        Assert.Equal(chainId, account.ChainId.Value);
    }
}