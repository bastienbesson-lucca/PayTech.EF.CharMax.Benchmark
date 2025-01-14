using Microsoft.EntityFrameworkCore;

namespace EFAsyncNvarcharMax;

public class TestContext : DbContext
{
    public static string ConnectionString =
        @"Data Source=.;Initial Catalog=PayTechDemo;Integrated Security=True;Encrypt=False;Packet Size=32768";
    
    // Packet Size Can be overridden to 32k for Unencrypted, 16k otherwise
    //@"Data Source=.;Initial Catalog=PayTechDemo;Integrated Security=True;Encrypt=False;Packet Size=32768";
    
    public DbSet<TestItem> Items { get; set; }
    
    public TestContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();

        optionsBuilder.UseSqlServer(
            connectionString: TestContext.ConnectionString);
        base.OnConfiguring(optionsBuilder);
        
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // If possible, use max length convention, will also avoid DDOS attack
        configurationBuilder.Properties<string>().HaveMaxLength(511);
    }
}

public class TestItem
{
    public int Id { get; set; }
    public string LargeTextBlob { get; set; }
}