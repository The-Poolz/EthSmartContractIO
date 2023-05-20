using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Managers.Tests;

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
