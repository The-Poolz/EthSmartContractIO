using Xunit;
using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasLimitExceededExceptionTests : ExceptionSerializationTestBase
{
    [Fact]
    internal void GasPriceExceededException_SerializationTest()
    {
        var message = "Gas limit exceeded.";
        var context = new StreamingContext();

        var exception = new TestableGasLimitExceededException(GetSerializationInfo<GasLimitExceededException>(message), context);

        var infoForGetObjectData = new SerializationInfo(typeof(GasLimitExceededException), new FormatterConverter());
        exception.GetObjectData(infoForGetObjectData, context);

        Assert.Equal(message, exception.Message);
    }
}
