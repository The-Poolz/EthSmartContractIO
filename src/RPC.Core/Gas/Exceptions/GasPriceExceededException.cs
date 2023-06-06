namespace RPC.Core.Gas.Exceptions;

public class GasPriceExceededException : GasExceptionBase
{
    public GasPriceExceededException() : base("Gas price exceeded.") { }
}
