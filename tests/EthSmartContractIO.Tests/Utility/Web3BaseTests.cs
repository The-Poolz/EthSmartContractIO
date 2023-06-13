using Xunit;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace EthSmartContractIO.Utility.Tests;

public class Web3BaseTests
{
    [Fact]
    internal void CreateWeb3()
    {
        string privateKey = "0x4e3c79ee2f53da4e456cb13887f4a7d59488677e9e48b6fb6701832df828f7e9";
        string rpcConnection = "http://localhost:8545/";
        Account account = new(privateKey);

        var web3 = Web3Base.CreateWeb3(rpcConnection, account);

        Assert.NotNull(web3);
        Assert.IsType<Web3>(web3);
    }
}
