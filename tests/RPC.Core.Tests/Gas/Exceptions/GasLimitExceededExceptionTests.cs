using Xunit;
using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasLimitExceededExceptionTests : ExceptionSerializationTestBase<GasLimitExceededException, TestableGasLimitExceededException>
{
    [Fact]
    internal void GasPriceExceededException_SerializationTest()
    {
        RunSerializationTest("Gas limit exceeded.");
    }

    protected override TestableGasLimitExceededException CreateTestableException(SerializationInfo info, StreamingContext context) =>
        new(info, context);
}
