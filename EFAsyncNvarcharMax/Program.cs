using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace EFAsyncNvarcharMax
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance;
            var summary = BenchmarkRunner.Run<Benchmark_EF>(config, args);
        }
    }
}