using Xunit;
using RPC.Core.Tests.Mocks;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Managers.Tests;

public class AccountManagerTests
{
    [Theory]
    [InlineData(0, 56, "0xE433FfB237950BddCd4a2CD86515B43cea1fDCC8")]
    [InlineData(1, 56, "0xD31e497678B4269EE553A819C505D4a78d0CBF6A")]
    internal void GetWallet_ExpectedAddress(int id, int chainId, string address)
    {
        var manager = new AccountManager(new MockMnemonicProvider());

        var account = manager.GetAccount(id, new HexBigInteger(chainId));

        Assert.NotNull(account);
        Assert.NotEqual(string.Empty, account.PrivateKey);
        Assert.NotEqual(string.Empty, account.PublicKey);
        Assert.NotEqual(string.Empty, account.Address);
        Assert.Equal(address, account.Address);
    }
}
