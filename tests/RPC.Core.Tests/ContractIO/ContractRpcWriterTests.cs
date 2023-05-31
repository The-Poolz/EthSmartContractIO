using Xunit;
using RPC.Core.Tests.Mocks;
using RPC.Core.Models;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcWriterTests
{
    private const string RpcUrl = "http://localhost:8545/";
    private const int AccountId = 0;

    [Fact]
    internal void ExecuteAction_ShouldReturnExpectedTransactionHash()
    {
        var gasSettings = new GasSettings()
        {
            MaxGasLimit = 21000,
            MaxGweiGasPrice = 10,
        };
        var request = new Request(RpcUrl, AccountId, 1, MockTransactionInput.MockTx.From, MockTransactionInput.MockTx.To, MockTransactionInput.MockTx.Value, gasSettings);
        var writeService = new ContractRpcWriter(RpcUrl, AccountId, MockSecretManager.GetMock);

        var txHash = writeService.ExecuteAction(request);

        Assert.NotNull(txHash);
        Assert.Equal("transactionHash", txHash);
    }
}
