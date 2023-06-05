using Xunit;
using RPC.Core.Models;
using FluentValidation;
using Nethereum.Hex.HexTypes;
using FluentValidation.TestHelper;

namespace RPC.Core.Validation.Tests;

public class WriteRequestValidatorTests
{
    private static readonly WriteRequestValidator validator = new();
    private static readonly string validRpcUrl = "http://localhost:8545";
    private static readonly string validEthereumAddress = "0xAb5801a7D398351b8bE11C439e05C5B3259aeC9B";
    private static readonly int validAccountId = 1;
    private static readonly uint validChainId = 1;
    private static readonly HexBigInteger validValue = new(10);
    private static readonly GasSettings validGasSettings = new(20000, 10);

    [Fact]
    internal void Write_ShouldNotHaveValidationError()
    {
        var request = new RpcRequest(validRpcUrl, validAccountId, validChainId, validEthereumAddress, validValue, validGasSettings);

        var result = validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(x => x.RpcUrl);
        result.ShouldNotHaveValidationErrorFor(x => x.AccountId);
        result.ShouldNotHaveValidationErrorFor(x => x.To);
        result.ShouldNotHaveValidationErrorFor(x => x.Value);
        result.ShouldNotHaveValidationErrorFor(x => x.GasSettings);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    internal void Write_ShouldHaveValidationError_WhenInvalidParametersArePassed(string rpcUrl, int accountId, uint chainId, string to, HexBigInteger value, GasSettings gasSettings, string expectedErrorMessage)
    {
        void testCode() => new RpcRequest(rpcUrl, accountId, chainId, to, value, gasSettings);

        var exception = Assert.Throws<ValidationException>(testCode);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { "", validAccountId, validChainId, validEthereumAddress, validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- RpcUrl: 'Rpc Url' must not be empty. Severity: Error{Environment.NewLine} -- RpcUrl: Invalid URL. Severity: Error" },
            new object[] { "invalid url", validAccountId, validChainId, validEthereumAddress, validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- RpcUrl: Invalid URL. Severity: Error" },
            new object[] { validRpcUrl, validAccountId, 0, validEthereumAddress, validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- ChainId: 'Chain Id' must not be equal to '0'. Severity: Error" },
            new object[] { validRpcUrl, validAccountId, validChainId, "", validValue, validGasSettings, $"Validation failed: {Environment.NewLine} -- To: 'To' must not be empty. Severity: Error{Environment.NewLine} -- To: Parameter 'To' is invalid ethereum address. Severity: Error" },
            new object[] { validRpcUrl, validAccountId, validChainId, validEthereumAddress, validValue, null, $"Validation failed: {Environment.NewLine} -- GasSettings: 'Gas Settings' must not be empty. Severity: Error" }
        };
}
