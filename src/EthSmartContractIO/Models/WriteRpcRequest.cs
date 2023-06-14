using System.Numerics;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.Models;

/// <summary>
/// Class for managing a write RPC request.
/// </summary>
public class WriteRpcRequest
{
    public BigInteger ChainId => AccountProvider.Account.ChainId!.Value;
    public HexBigInteger Value { get; private set; }
    public GasSettings GasSettings { get; private set; }
    public IAccountProvider AccountProvider { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteRpcRequest"/> class.
    /// </summary>
    /// <param name="value">The value to send with the request.</param>
    /// <param name="gasSettings">The settings for gas usage.</param>
    /// <param name="accountProvider">The account provider to use.</param>
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
