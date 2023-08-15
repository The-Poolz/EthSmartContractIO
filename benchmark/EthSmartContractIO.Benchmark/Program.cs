using BenchmarkDotNet.Running;
using EthSmartContractIO.Benchmark.ContractIO;

namespace EthSmartContractIO.Benchmark;
class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ContractReaderBenchmark>();
    }
}
