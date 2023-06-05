using RPC.Core.Providers;

namespace RPC.Core.Tests.Mocks;

internal class MockMnemonicProvider : IMnemonicProvider
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

    public string GetMnemonic() => string.Join(" ", MnemonicWords);
}
