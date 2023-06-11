using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.AccountProvider;

public class PrivateKeyAccountProvider : IAccountProvider
{
    public Account Account { get; private set; }
    public PrivateKeyAccountProvider(params string[] values)
    {
        if (uint.TryParse(values[1], out var result))
        {
            Account = GetAccount(values[0].ToString(), result);
        }
        else
        {
            throw new ArgumentException("ChainId must be a number");
        }
    }

    public PrivateKeyAccountProvider(string privateKey, uint chainId)
    {
        Account = GetAccount(privateKey, chainId);
    }

    internal static Account GetAccount(string privateKey, uint chainId) => new(privateKey, new HexBigInteger(chainId));
}