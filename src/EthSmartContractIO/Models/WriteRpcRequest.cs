using System.Numerics;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.Models;

public class WriteRpcRequest
{
    public BigInteger ChainId => AccountProvider.Account.ChainId!.Value;
    public HexBigInteger Value { get; private set; }
    public GasSettings GasSettings { get; private set; }
    public IAccountProvider AccountProvider { get; private set; }

    public WriteRpcRequest(
        HexBigInteger value,
        GasSettings gasSettings,
        IAccountProvider accountProvider
    )
    {
        Value = value;
        GasSettings = gasSettings;
        AccountProvider = accountProvider;
    }
}
