```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2605)
13th Gen Intel Core i7-13800H, 1 CPU, 20 logical and 14 physical cores
.NET SDK 8.0.404
  [Host] : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2


```
| Method             | Mean | Error |
|------------------- |-----:|------:|
| GetWithToList      |   NA |    NA |
| GetWithToListAsync |   NA |    NA |

Benchmarks with issues:
  Benchmark_EF.GetWithToList: DefaultJob
  Benchmark_EF.GetWithToListAsync: DefaultJob
