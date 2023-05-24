using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Managers.Tests;

[Collection("RESET_ENVIRONMENT")]
public class WalletManagerTests
{
    private const string MnemonicIdEnvName = "SECRET_MNEMONIC_ID";
    private const string MnemonicKeyEnvName = "SECRET_MNEMONIC_KEY";
    private static string GetExceptionMessage(string environmentVariable) =>
        $"Environment variable '{environmentVariable}' is null or empty.";

    [Fact]
    internal void GetWallet_ExpectedWords()
    {
        Environment.SetEnvironmentVariable(MnemonicIdEnvName, "Mnemonic");
        Environment.SetEnvironmentVariable(MnemonicKeyEnvName, "words");

        var wallet = WalletManager.GetWallet(MockSecretManager.GetMock);

        Assert.NotNull(wallet);
        Assert.Equal(MockSecretManager.MnemonicWords, wallet.Words);
    }

    [Fact]
    internal void GetWallet_SecretMnemonicId_TrownException()
    {
        Environment.SetEnvironmentVariable(MnemonicIdEnvName, "");
        Environment.SetEnvironmentVariable(MnemonicKeyEnvName, "words");

        Action testCode = () => WalletManager.GetWallet(MockSecretManager.GetMock);

        var exception = Assert.Throws<InvalidOperationException>(testCode);
        Assert.Equal(GetExceptionMessage(MnemonicIdEnvName), exception.Message);
    }

    [Fact]
    internal void GetWallet_SecretMnemonicKey_TrownException()
    {
        Environment.SetEnvironmentVariable(MnemonicIdEnvName, "Mnemonic");
        Environment.SetEnvironmentVariable(MnemonicKeyEnvName, "");

        Action testCode = () => WalletManager.GetWallet(MockSecretManager.GetMock);

        var exception = Assert.Throws<InvalidOperationException>(testCode);
        Assert.Equal(GetExceptionMessage(MnemonicKeyEnvName), exception.Message);
    }
}
