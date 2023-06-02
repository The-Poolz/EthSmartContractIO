using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Providers.Tests;

public class WalletProviderTests
{
    [Fact]
    internal void GetWallet_ExpectedWords()
    {
        var wallet = WalletProvider.GetWallet(new MockMnemonicProvider());

        Assert.NotNull(wallet);
        Assert.Equal(MockMnemonicProvider.MnemonicWords, wallet.Words);
    }
}
