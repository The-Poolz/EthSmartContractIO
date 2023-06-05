using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class TestableGasPriceExceededException : GasPriceExceededException
{
    public TestableGasPriceExceededException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
