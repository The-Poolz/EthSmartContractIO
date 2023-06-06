using RPC.Core.Providers;

namespace RPC.Core.Tests.Mocks;

internal static class MockMnemonicProvider
{
    private const string mnemonicWords = "believe awake season ahead air royal patrol annual found habit shed choice";
    internal static readonly MnemonicProvider MnemonicProvider = new(mnemonicWords, 0, 1);
}
