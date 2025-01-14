using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace EFAsyncNvarcharMax;

[MemoryDiagnoser(false)]
public class Benchmark_EF
{
    private int id;

    public Benchmark_EF()
    {
        // Seed DATABASE with one row of 5MB
        using var context = new TestContext();
        var array = new char[5 * 1024 * 1024]; // 5MB
        var random = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (char)random.Next(32, 126);
        }
        
        var entity = new TestItem() { LargeTextBlob = new string(array) };
        context.Items.Add(entity);
        context.SaveChanges();
        
        this.id = entity.Id;
    }

    [Benchmark]
    public void GetWithToList()
    {
        using var context = new TestContext();
        var entity = context.Items.Where(b => b.Id == this.id).ToList();
    }

    [Benchmark]
    public async Task GetWithToListAsync()
    {
        await using var context = new TestContext();
        var entity = await context.Items.Where(b => b.Id == this.id).ToListAsync();
    }
}