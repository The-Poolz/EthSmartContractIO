using System.Numerics;
using Nethereum.Hex.HexTypes;
using Net.Web3.EthereumWallet;
using EthSmartContractIO.Extensions;

namespace EthSmartContractIO.Builders;

public class DataBuilder
{
    private string data;

    public DataBuilder(string functionName)
    {
        data = functionName.ToMethodSignature();
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