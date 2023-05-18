using Nethereum.Web3;

namespace RPC.Core;

public class RPC
{
    private readonly string url;

    public RPC(string connectionOfRPC)
    {
        url = connectionOfRPC;
    }

    public void ReadFromNetwork() => throw new NotImplementedException();
    public void WriteToNetwork() => throw new NotImplementedException();
}
