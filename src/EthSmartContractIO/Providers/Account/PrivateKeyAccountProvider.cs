using Nethereum.Hex.HexTypes;

namespace EthSmartContractIO.Providers.Account;

public class PrivateKeyAccountProvider : IAccountProvider
{
    private readonly string privateKey;
    private readonly uint chainId;

    public PrivateKeyAccountProvider(params object[] args)
    {
        if (args.Length != 2)
            throw new ArgumentException("Two arguments required: private key and chain id");

        if (args[0] is string privateKey)
            this.privateKey = privateKey;
        else
            throw new ArgumentException("The first argument must be a (string) private key", nameof(args));

        if (args[1] is uint chainId)
            this.chainId = chainId;
        else
            throw new ArgumentException("The second argument must be a (uint) chain ID", nameof(args));
    }

    public Nethereum.Web3.Accounts.Account GetAccount(params object[] args) =>
        new(privateKey, new HexBigInteger(chainId));
}
