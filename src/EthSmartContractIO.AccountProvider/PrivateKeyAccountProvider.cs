using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.AccountProvider;

public class PrivateKeyAccountProvider : IAccountProvider
{
    public Account Account { get; private set; }

    public PrivateKeyAccountProvider(string privateKey, uint chainId)
    {
        Account = new Account(privateKey, new HexBigInteger(chainId));
    }
}