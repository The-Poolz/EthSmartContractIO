using Xunit;
using System.Numerics;
using FluentAssertions;
using Net.Web3.EthereumWallet;
using EthSmartContractIO.Builders;
using EthSmartContractIO.Extensions;

namespace EthSmartContractIO.Tests.Builders;

public class DataBuilderTests
{
    private const string methodName = "transfer(address,uint256)";
    private readonly string methodSignature = methodName.ToMethodSignature();
    private readonly BigInteger bigInt = new(12345);
    private readonly EthereumAddress ethereumAddress = new("0x1234567890123456789012345678901234567890");
    private readonly DataBuilder dataBuilder = new(methodName);

    [Fact]
    internal void Constructor_InitializesMethodSignatureCorrectly()
    {
        var result = dataBuilder.Build();

        result.Should().StartWith(methodSignature);
    }

    [Fact]
    internal void WithBigInteger_AppendsBigIntegerCorrectly()
    {
        var expected = bigInt.ToString("X").PadLeft(64, '0');

        var result = dataBuilder.WithBigInteger(bigInt)
            .Build();

        result.Should().Contain(expected);
    }

    [Fact]
    internal void WithAddress_AppendsEthereumAddressCorrectly()
    {
        var expected = ethereumAddress.Address[2..].PadLeft(64, '0');

        var result = dataBuilder.WithAddress(ethereumAddress)
            .Build();

        result.Should().Contain(expected);
    }

    [Fact]
    internal void Build_CreatesCorrectDataString()
    {
        var expectedBigInteger = bigInt.ToString("X").PadLeft(64, '0');
        var expectedAddress = ethereumAddress.Address[2..].PadLeft(64, '0');

        var expectedData = $"{methodSignature}{expectedAddress}{expectedBigInteger}";

        var result = dataBuilder
            .WithAddress(ethereumAddress)
            .WithBigInteger(bigInt)
            .Build();

        result.Should().Be(expectedData);
    }
}