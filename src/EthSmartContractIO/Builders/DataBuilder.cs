using System.Numerics;
using EthSmartContractIO.Extensions;
using Net.Web3.EthereumWallet;
using Nethereum.Hex.HexTypes;

namespace EthSmartContractIO.Builders;

public class DataBuilder
{
    private string data;

    public DataBuilder(string method)
    {
        data = method.ToMethodSignature();
    }

    public DataBuilder WithBigInteger(BigInteger parameter)
    {
        data += new HexBigInteger(parameter).HexValue[2..].PadLeft(64, '0');
        return this;
    }

    public DataBuilder WithAddress(EthereumAddress parameter)
    {
        data += parameter.Address[2..].PadLeft(64, '0');
        return this;
    }

    public string Build() => data;
}