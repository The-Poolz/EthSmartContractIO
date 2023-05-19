using Moq;
using SecretsManager;

namespace RPC.Core.Tests.Mocks;

internal static class MockSecretManager
{
    public static string[] MnemonicWords =>
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

    public static SecretManager GetMock
    {
        get
        {
            var secretManager = new Mock<SecretManager>();
            secretManager.Setup(x => x.GetSecretValue("Mnemonic", "string"))
                .Returns(string.Join(' ', MnemonicWords));

            return secretManager.Object;
        }
    }
}
