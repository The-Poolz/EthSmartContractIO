using Xunit;
using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasLimitExceededExceptionTests : ExceptionSerializationTestBase<GasLimitExceededException, TestableGasLimitExceededException>
{
    [Fact]
    internal void GasPriceExceededException_SerializationTest()
    {
        var expectedMessage = "Gas limit exceeded.";

        RunSerializationTest(expectedMessage);

        var actualMessage = infoForGetObjectData.GetString("Message");
        Assert.Equal(expectedMessage, actualMessage);
    }

    protected override TestableGasLimitExceededException CreateTestableException(SerializationInfo info, StreamingContext context) =>
        new(info, context);
}
