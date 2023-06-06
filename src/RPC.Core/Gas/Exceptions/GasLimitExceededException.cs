using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions;

[Serializable]
public class GasLimitExceededException : Exception
{
    public GasLimitExceededException() : base("Gas limit exceeded.") { }

    protected GasLimitExceededException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
