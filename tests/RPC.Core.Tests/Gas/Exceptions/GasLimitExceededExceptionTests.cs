using Xunit;
using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasLimitExceededExceptionTests
{
    private class TestableGasLimitExceededException : GasLimitExceededException
    {
        public TestableGasLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    [Fact]
    public void GasLimitExceededException_SerializationTest()
    {
        var info = new SerializationInfo(typeof(GasLimitExceededException), new FormatterConverter());
        var context = new StreamingContext();

        var exception = new TestableGasLimitExceededException(info, context);
        exception.GetObjectData(info, context);
    }
}
