using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions;

public class GasLimitExceededException : Exception, ISerializable
{
    public GasLimitExceededException() : base("Gas limit exceeded.") { }
}
