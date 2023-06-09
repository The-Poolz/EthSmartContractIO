using Xunit;
using FluentValidation;
using Nethereum.Hex.HexTypes;
using EthSmartContractIO.Models;
using FluentValidation.TestHelper;
using EthSmartContractIO.Tests.Mocks;

namespace EthSmartContractIO.Validation.Tests;

public class WriteRequestValidatorTests
{
    private static readonly WriteRequestValidator validator = new();
    private static readonly string validRpcUrl = "http://localhost:8545";
    private static readonly string validEthereumAddress = "0xAb5801a7D398351b8bE11C439e05C5B3259aeC9B";
    private static readonly uint validChainId = 1;
    private static readonly HexBigInteger validValue = new(10);
    private static readonly GasSettings validGasSettings = new(20000, 10);

    [Fact]
    internal void Write_ShouldNotHaveValidationError()
    {
        var request = new RpcRequest(
            rpcUrl: validRpcUrl,
            to: validEthereumAddress,
            writeRequest: new WriteRpcRequest(
                chainId: validChainId,
                value: validValue,
                gasSettings: validGasSettings,
                accountProvider: new MockAccountProvider()
            )
        );

        var result = validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(x => x.RpcUrl);
        result.ShouldNotHaveValidationErrorFor(x => x.To);
        result.ShouldNotHaveValidationErrorFor(x => x.WriteRequest!.Value);
        result.ShouldNotHaveValidationErrorFor(x => x.WriteRequest!.GasSettings);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    internal void Write_ShouldHaveValidationError_WhenInvalidParametersArePassed(string rpcUrl, uint chainId, string to, HexBigInteger value, GasSettings gasSettings, string expectedErrorMessage)
    {
        Action testCode = () => _ = new RpcRequest(
            rpcUrl: rpcUrl,
            to: to,
            writeRequest: new WriteRpcRequest(
                chainId: chainId,
                value: value,
                gasSettings: gasSettings,
                accountProvider: new MockAccountProvider()
            )
        );

        var exception = Assert.Throws<ValidationException>(testCode);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { "", validChainId, validEthereumAddress, validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- RpcUrl: 'Rpc Url' must not be empty. Severity: Error{Environment.NewLine} -- RpcUrl: Invalid URL. Severity: Error" },
            new object[] { "invalid url", validChainId, validEthereumAddress, validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- RpcUrl: Invalid URL. Severity: Error" },
            new object[] { validRpcUrl, 0, validEthereumAddress, validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- WriteRequest.ChainId: 'Write Request Chain Id' must not be equal to '0'. Severity: Error" },
            new object[] { validRpcUrl, validChainId, "", validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- To: 'To' must not be empty. Severity: Error{Environment.NewLine} -- To: Parameter 'To' is invalid ethereum address. Severity: Error" },
        };
}
