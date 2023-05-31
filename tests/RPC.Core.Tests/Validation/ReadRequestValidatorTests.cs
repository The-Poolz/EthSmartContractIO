using Xunit;
using RPC.Core.Models;
using FluentValidation;
using FluentValidation.TestHelper;

namespace RPC.Core.Validation.Tests;

public class ReadRequestValidatorTests
{
    private static readonly ReadRequestValidator validator = new();
    private static readonly string validEthereumAddress = "0xAb5801a7D398351b8bE11C439e05C5B3259aeC9B";
    private static readonly string validData = "0x0a0b0c0d";
    private static readonly string validRpcUrl = "http://localhost:8545";

    [Fact]
    internal void Read_ShouldNotHaveValidationError()
    {
        var request = new Request(validRpcUrl, validEthereumAddress, validData);

        var result = validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(x => x.RpcUrl);
        result.ShouldNotHaveValidationErrorFor(x => x.To);
        result.ShouldNotHaveValidationErrorFor(x => x.Data);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    internal void Request_ShouldHaveValidationError_WhenInvalidParametersArePassed(string rpcUrl, string ethereumAddress, string data, string expectedErrorMessage)
    {
        Action testCode = () => new Request(rpcUrl, ethereumAddress, data);

        var exception = Assert.Throws<ValidationException>(testCode);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { "", validEthereumAddress, validData, $"Validation failed: {Environment.NewLine} -- RpcUrl: 'Rpc Url' must not be empty. Severity: Error{Environment.NewLine} -- RpcUrl: Invalid URL. Severity: Error" },
            new object[] { "invalid url", validEthereumAddress, validData, $"Validation failed: {Environment.NewLine} -- RpcUrl: Invalid URL. Severity: Error" },
            new object[] { validRpcUrl, "", validData, $"Validation failed: {Environment.NewLine} -- To: 'To' must not be empty. Severity: Error{Environment.NewLine} -- To: Parameter 'To' is invalid ethereum address. Severity: Error" },
            new object[] { validRpcUrl, "invalid ethereum address", validData, $"Validation failed: {Environment.NewLine} -- To: Parameter 'To' is invalid ethereum address. Severity: Error" },
            new object[] { validRpcUrl, validEthereumAddress, "", $"Validation failed: {Environment.NewLine} -- Data: 'Data' must not be empty. Severity: Error{Environment.NewLine} -- Data: Parameter 'Data' not correctly formatted. Severity: Error" },
            new object[] { validRpcUrl, validEthereumAddress, "invalid data", $"Validation failed: {Environment.NewLine} -- Data: Parameter 'Data' not correctly formatted. Severity: Error" }
        };
}
