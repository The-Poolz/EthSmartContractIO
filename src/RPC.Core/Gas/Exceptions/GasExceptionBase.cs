using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions;

[Serializable]
public abstract class GasExceptionBase : Exception
{
    protected GasExceptionBase(string message)
        : base(message)
    { }

    protected GasExceptionBase(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
