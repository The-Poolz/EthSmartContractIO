using Xunit;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.Gas.Tests;

public class GasPricerTests
{
    [Fact]
    internal void GetCurrentWeiGasPrice_ExpectedGasPrice()
    {
        var gasPricer = new GasPricer(MockWeb3.GetMock);

        var gasPrice = gasPricer.GetCurrentWeiGasPrice();

        Assert.Equal(new HexBigInteger(5000000000), gasPrice);
    }
}
