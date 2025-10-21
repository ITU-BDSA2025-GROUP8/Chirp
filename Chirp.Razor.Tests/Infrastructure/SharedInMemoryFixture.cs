using Chirp.Razor.data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Chirp.Razor.Test.Infrastructure;

//Shared SQLite in-memory database for tests
//Based on Listing 36.16 logic from "ASP.NET Core in Action"
public sealed class SharedInMemoryFixture : IAsyncLifetime
{
    private SqliteConnection _connection = default!;
    public DbContextOptions<ChirpDBContext> Options { get; private set; } = default!;
    
    //Run before any tests
    public async Task InitializeAsync() 
    {
        //Create and open shared in-memory SQLite connection
        _connection = new SqliteConnection("DataSource=:memory:");
        await _connection.OpenAsync();

        //Configure EF Core to use the shared connection
        Options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(_connection)
            .Options;

        //Create database schema
        using var context = new ChirpDBContext(Options);
        await context.Database.EnsureCreatedAsync();
    }
    //Run after all tests
    public async Task DisposeAsync(){
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
}
    //helper method to create new DbContext instances sharing same open connection
    public ChirpDBContext CreateContext()
    {
        //Create new context instance
        var builder = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(_connection);
        return new ChirpDBContext(builder.Options);
    }
}
//Register the fixture with xUnit so multiple test classes can share it 
//idea from ChatGPT based on xUnit documentation
[CollectionDefinition("sqlite")]
public sealed class SqliteCollection : ICollectionFixture<SharedInMemoryFixture> { }
