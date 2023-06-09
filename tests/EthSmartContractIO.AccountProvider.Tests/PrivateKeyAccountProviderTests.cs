using Xunit;

namespace EthSmartContractIO.AccountProvider.Tests;

public class PrivateKeyAccountProviderTests
{
    [Fact]
    internal void Ctor_InitializesAccountWithGivenPrivateKeyAndChainId()
    {
        var privateKey = "0x1234";
        var chainId = 1u;

        var account = new PrivateKeyAccountProvider(privateKey, chainId).Account;

        Assert.NotNull(account);
        Assert.Equal(privateKey, account.PrivateKey);
        Assert.NotNull(account.ChainId);
        Assert.Equal(chainId, account.ChainId.Value);
    }
}
