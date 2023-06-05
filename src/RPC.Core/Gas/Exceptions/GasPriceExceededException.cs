using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions;

public class GasPriceExceededException : Exception, ISerializable
{
    public GasPriceExceededException() : base("Gas price exceeded.") { }
}
