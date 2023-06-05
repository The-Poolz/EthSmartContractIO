using Xunit;
using RPC.Core.Models;
using RPC.Core.Tests.Mocks;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace RPC.Core.ContractIO.Tests;

[Collection("RESET_ENVIRONMENT")]
public class ContractRpcWriterTests
{
    private RpcRequest request;

    public ContractRpcWriterTests()
    {
        request = new RpcRequest(
            rpcUrl: "http://localhost:8545/",
            accountId: 0,
            chainId: 1,
            to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            value: new HexBigInteger(10000000000000000),
            gasSettings: new GasSettings(30000, 6)
        );
    }

    [Fact]
    internal void InitializeWeb3()
    {
        Environment.SetEnvironmentVariable("SECRET_MNEMONIC_ID", "Mnemonic");
        Environment.SetEnvironmentVariable("SECRET_MNEMONIC_KEY", "words");
        var contractRpcWriter = new ContractRpcWriter(request)
        {
            SecretManager = MockSecretManager.GetMock
        };

        var result = contractRpcWriter.InitializeWeb3();

        Assert.NotNull(result);
        Assert.IsType<Web3>(result);
    }

    [Fact]
    internal void RunContractAction_ExpectedTransactionHex()
    {
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
