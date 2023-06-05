using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions;

[Serializable]
public class GasPriceExceededException : Exception
{
    public GasPriceExceededException() : base("Gas price exceeded.") { }

    protected GasPriceExceededException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
