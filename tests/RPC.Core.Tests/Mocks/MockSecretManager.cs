using Moq;
using SecretsManager;

namespace RPC.Core.Tests.Mocks;

internal static class MockSecretManager
{
    internal static string[] MnemonicWords =>
        new string[]
        {
            "vibrant",
            "grant",
            "code",
            "warm",
            "help",
            "hedgehog",
            "fit",
            "bird",
            "judge",
            "blade",
            "cliff",
            "curious"
        };

    internal static SecretManager GetMock
    {
        get
        {
            var secretManager = new Mock<SecretManager>();
            secretManager.Setup(x => x.GetSecretValue("Mnemonic", "words"))
                .Returns(string.Join(' ', MnemonicWords));

            return secretManager.Object;
        }
    }
}
