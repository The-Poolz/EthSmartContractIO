using RPC.Core.Providers;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using SmartContractIO.SecretsProvider;

namespace SmartContractIO.AccountProvider;

public class PrivateKeyAccountProvider : IAccountProvider
{
    public Account Account { get; private set; }

    public PrivateKeyAccountProvider(string privateKey, uint chainId)
    {
        Account = new Account(privateKey, new HexBigInteger(chainId));
    }

    public PrivateKeyAccountProvider(ISecretsProvider secretsProvider, uint chainId)
    {
        Account = new Account(secretsProvider.Secret, new HexBigInteger(chainId));
    }
}