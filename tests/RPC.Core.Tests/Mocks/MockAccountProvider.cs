using RPC.Core.Providers;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Tests.Mocks;

internal class MockAccountProvider : IAccountProvider
{
    public Account Account => new("0x4e3c79ee2f53da4e456cb13887f4a7d59488677e9e48b6fb6701832df828f7e9");
}
