using Xunit;
using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasPriceExceededExceptionTests : ExceptionSerializationTestBase
{
    [Fact]
    internal void GasPriceExceededException_SerializationTest()
    {
        var message = "Gas price exceeded.";
        var context = new StreamingContext();

        var exception = new TestableGasPriceExceededException(GetSerializationInfo<GasPriceExceededException>(message), context);

        var infoForGetObjectData = new SerializationInfo(typeof(GasPriceExceededException), new FormatterConverter());
        exception.GetObjectData(infoForGetObjectData, context);

        Assert.Equal(message, exception.Message);
    }
}
