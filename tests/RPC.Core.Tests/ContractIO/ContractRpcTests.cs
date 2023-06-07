using Xunit;
using RPC.Core.Models;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;
using RPC.Core.Tests.Mocks;
using Org.BouncyCastle.Asn1.Ocsp;
using Nethereum.Hex.HexTypes;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Web3;

namespace RPC.Core.ContractIO.Tests;

public class ContractRpcTests
{
    private const string RpcUrl = "http://localhost:8545/";

    [Fact]
    internal void ExecuteAction_Read_ExpectedJsonString()
    {
        var request = new RpcRequest(RpcUrl, "0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");
        var response = new JObject()
        {
            { "jsonrpc", "2.0" },
            { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
            { "id", 0 }
        };
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .RespondWithJson(response);

        var result = new ContractRpc().ExecuteAction(request);

        Assert.NotNull(result);
        Assert.Equal(response["result"]?.ToString(), result);
    }

    [Fact]
    internal void ExecuteAction_Write_ExpectedTransactionHex()
    {
        var request = new RpcRequest(
            rpcUrl: "http://localhost:8545/",
            to: "0xA98b8386a806966c959C35c636b929FE7c5dD7dE",
            writeRequest: new WriteRpcRequest(
                chainId: 1,
                value: new HexBigInteger(10000000000000000),
                gasSettings: new GasSettings(30000, 6),
                accountProvider: MockMnemonicProvider.MnemonicProvider
            )
        );
        var contractRpc = new ContractRpc();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IWeb3>(MockWeb3.GetMock);
        contractRpc.ServiceProvider = serviceCollection.BuildServiceProvider();

        var result = contractRpc.ExecuteAction(request);

        Assert.NotNull(result);
        Assert.Equal("transactionHash", result);
    }
}
