using RPC.Core.Providers;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace SmartContractIO.AccountProvider;

public class PrivateKeyAccountProvider : IAccountProvider
{
    public Account Account { get; protected set; }

    public PrivateKeyAccountProvider(string privateKey, uint chainId)
    {
        Account = new Account(privateKey, new HexBigInteger(chainId));
    }
}