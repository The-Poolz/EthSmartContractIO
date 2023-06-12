using FluentValidation;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Providers.Account.Validation;

namespace EthSmartContractIO.Providers.Account;

public class PrivateKeyAccountProvider : IAccountProvider
{
    private readonly string privateKey;
    private readonly uint chainId;

    public PrivateKeyAccountProvider(params object[] args)
    {
        new PrivateKeyParamsValidator().ValidateAndThrow(args);

        privateKey = (string)args[0];
        chainId = (uint)args[1];
    }

    public Nethereum.Web3.Accounts.Account GetAccount(params object[] args) =>
        new(privateKey, new HexBigInteger(chainId));
}
