using Moq;
using Xunit;
using SecretsManager;
using RPC.Core.Managers;

namespace RPC.Core.Tests;

public class WalletManagerTests
{
    [Fact]
    public void Ctor()
    {
        var secretmanager = new Mock<SecretManager>();
        secretmanager.Setup(x => x.GetSecretValue("Mnemonic", "string"))
            .Returns("vibrant grant code warm help hedgehog fit bird judge blade cliff curious");

        var wallet = WalletManager.GetWallet(secretmanager.Object);

        Assert.NotNull(wallet);
    }
}
