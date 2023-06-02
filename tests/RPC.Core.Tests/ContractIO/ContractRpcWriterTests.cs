using Xunit;
using RPC.Core.Models;
using RPC.Core.Tests.Mocks;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcWriterTests
{
    [Fact]
    internal void RunContractAction_ExpectedTransactionHex()
    {
        var request = new Request(
            rpcUrl: "http://localhost:8545/",
            accountId: 0,
            chainId: 1,
            from: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            value: new HexBigInteger(10000000000000000),
            gasSettings: new GasSettings(29000, 10)
        );
        var contractRpcWriter = new ContractRpcWriter(request)
        {
            Web3 = MockWeb3.GetMock,
            SecretManager = MockSecretManager.GetMock
        };

        var result = contractRpcWriter.RunContractAction();

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }
}
