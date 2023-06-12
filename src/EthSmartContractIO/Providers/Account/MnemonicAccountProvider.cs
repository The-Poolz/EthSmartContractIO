using FluentValidation;
using Nethereum.HdWallet;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Providers.Account.Validation;

namespace EthSmartContractIO.Providers.Account;

public class MnemonicAccountProvider : IAccountProvider
{
    private readonly string mnemonicWords;
    private readonly uint accountId;
    private readonly uint chainId;
    private readonly string seedPassword = "";

    public MnemonicAccountProvider(params object[] args)
    {
        new MnemonicParamsValidator().ValidateAndThrow(args);

        mnemonicWords = (string)args[0];
        accountId = (uint)args[1];
        chainId = (uint)args[2];

        if (args.Length > 3 && args[3] is string sp)
            seedPassword = sp;
    }

    public Nethereum.Web3.Accounts.Account GetAccount(params object[] args)
    {
        var wallet = new Wallet(words: mnemonicWords, seedPassword: seedPassword);
        return wallet.GetAccount((int)accountId, new HexBigInteger(chainId));
    }
}