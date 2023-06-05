namespace RPC.Core.Gas.Exceptions;

public class GasLimitExceededException : Exception
{
    public GasLimitExceededException() : base("Gas limit exceeded.") { }
}
