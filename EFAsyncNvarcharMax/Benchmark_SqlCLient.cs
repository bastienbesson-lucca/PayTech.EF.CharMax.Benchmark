using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Data.SqlClient;

namespace EFAsyncNvarcharMax;

[MemoryDiagnoser(false)]
public class Benchmark_SqlCLient
{
    [GlobalSetup]
    public void Setup()
    {
        using var conn = new SqlConnection(TestContext.ConnectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
IF OBJECT_ID('dbo.SqlItem', 'U') IS NOT NULL
DROP TABLE SqlItem; 
CREATE TABLE SqlItem (id INT, foo VARBINARY(MAX))
";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO SqlItem (id, foo) VALUES (@id, @foo)";
        cmd.Parameters.AddWithValue("id", 1);
        cmd.Parameters.AddWithValue("foo", new byte[1024 * 1024 * 10]);
        cmd.ExecuteNonQuery();
    }
    
    [Benchmark]
    public async ValueTask<int> Async()
    {
        using var conn = new SqlConnection(TestContext.ConnectionString);
        using var cmd = new SqlCommand("SELECT foo FROM dbo.SqlItem", conn);
        await conn.OpenAsync();

        return ((byte[])await cmd.ExecuteScalarAsync()).Length;
    }

    [Benchmark]
    public async ValueTask<int> Sync()
    {
        using var conn = new SqlConnection(TestContext.ConnectionString);
        using var cmd = new SqlCommand("SELECT foo FROM dbo.SqlItem", conn);
        conn.Open();

        return ((byte[])cmd.ExecuteScalar()).Length;
    }
}