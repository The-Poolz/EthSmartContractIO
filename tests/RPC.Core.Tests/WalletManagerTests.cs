using Xunit;
using RPC.Core.Managers;
using RPC.Core.Tests.Data;

namespace RPC.Core.Tests;

public class WalletManagerTests
{
    [Fact]
    internal void GetWallet_ExpectedWords()
    {
        var wallet = WalletManager.GetWallet(MockSecretManager.GetMock);

        Assert.NotNull(wallet);
        Assert.Equal(MockSecretManager.MnemonicWords, wallet.Words);
    }
}
