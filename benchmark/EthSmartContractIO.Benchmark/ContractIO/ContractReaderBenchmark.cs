using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;
using EthSmartContractIO.Models;
using BenchmarkDotNet.Attributes;

namespace EthSmartContractIO.Benchmark.ContractIO;

public class ContractReaderBenchmark
{
    private const string RpcUrl = "http://localhost:8545/";
    private readonly RpcRequest request = new(RpcUrl, "0xA98b8386a806966c959C35c636b929FE7c5dD7dE", "0xbef7a2f0");
    private readonly JObject response = new()
    {
        { "jsonrpc", "2.0" },
        { "result", "0x000000000000000000000000000000000000000000000000002386f26fc10000" },
        { "id", 0 }
    };

    [Benchmark]
    public string NewContractReader()
    {
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .RespondWithJson(response);

        return new NewContractReader(request).RunContractAction();
    }

    [Benchmark]
    public string OldContractReader()
    {
        using var httpTest = new HttpTest();
        httpTest
            .ForCallsTo(RpcUrl)
            .RespondWithJson(response);

        return new OldContractReader(request).RunContractAction();
    }
}