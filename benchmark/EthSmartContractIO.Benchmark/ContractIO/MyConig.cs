using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace EthSmartContractIO.Benchmark.ContractIO
{
    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            AddJob(Job.Default
                .WithStrategy(RunStrategy.Monitoring) // Set the strategy to Monitoring
                .WithInvocationCount(3000) // Number of invocations per iteration
                .WithUnrollFactor(30) // Number of benchmark method invocations per one iteration of a generated loop
                .WithMinIterationCount(30)); // Number of iterations to run
        }
    }
}
