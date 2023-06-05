namespace RPC.Core.Gas.Exceptions;

public class GasPriceExceededException : Exception
{
    public GasPriceExceededException() : base("Gas price exceeded.") { }
}
