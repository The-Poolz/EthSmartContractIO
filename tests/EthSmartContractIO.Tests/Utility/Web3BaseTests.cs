using Xunit;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace EthSmartContractIO.Utility.Tests;

public class Web3BaseTests
{
    [Fact]
    internal void CreateWeb3()
    {
        string rpcConnection = "http://localhost:8545/";
        Account account = new("0xprivateKey");

        var web3 = Web3Base.CreateWeb3(rpcConnection, account);

        Assert.NotNull(web3);
        Assert.IsType<Web3>(web3);
    }
}
