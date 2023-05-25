using Xunit;
using RPC.Core.Tests.Mocks;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Gas.Tests;

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
