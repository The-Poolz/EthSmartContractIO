using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class TestableGasExceptionBase : GasExceptionBase
{
    public TestableGasExceptionBase(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
