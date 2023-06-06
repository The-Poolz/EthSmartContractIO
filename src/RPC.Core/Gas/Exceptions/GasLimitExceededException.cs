namespace RPC.Core.Gas.Exceptions;

public class GasLimitExceededException : GasExceptionBase
{
    public GasLimitExceededException() : base("Gas limit exceeded.") { }
}
