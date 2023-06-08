using Xunit;

namespace SmartContractIO.AccountProvider.Tests;

public class MnemonicAccountProviderTests
{
    [Fact]
    internal void Ctor_InitializesAccountWithGivenMnemonicWordsAccountIdAndChainId()
    {
        var mnemonicWords = "abandon amount liar amount expire adjust cage candy arch gather drum buyer";
        var accountId = 0u;
        var chainId = 1u;

        var account = new MnemonicAccountProvider(mnemonicWords, accountId, chainId).Account;

        Assert.NotNull(account);
        Assert.NotNull(account.PrivateKey);
        Assert.NotNull(account.ChainId);
        Assert.Equal(chainId, account.ChainId.Value);
    }
}