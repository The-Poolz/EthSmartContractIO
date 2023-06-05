using Xunit;
using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasPriceExceededExceptionTests : ExceptionSerializationTestBase<GasPriceExceededException, TestableGasPriceExceededException>
{
    [Fact]
    internal void GasPriceExceededException_SerializationTest()
    {
        var expectedMessage = "Gas price exceeded.";

        RunSerializationTest(expectedMessage);

        var actualMessage = infoForGetObjectData.GetString("Message");
        Assert.Equal(expectedMessage, actualMessage);
    }

    protected override TestableGasPriceExceededException CreateTestableException(SerializationInfo info, StreamingContext context) =>
        new(info, context);
}
