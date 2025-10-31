using System.Data.Common;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Tests.Data;

public class DbInitializerTest : IDisposable
{
    //the set up is the same as in Chirp/tests/Chirp.Infrastructure.Tests/Repositories/AuthorRepositoryTest.cs
    private readonly DbConnection _connection;
    private readonly DbContextOptions<ChirpDBContext> _options;

    public DbInitializerTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(_connection)
            .Options;
    }

    ChirpDBContext CreateDbContext() => new ChirpDBContext(_options);

    public void Dispose() => _connection.Dispose();
    
    
    [Fact]
    public void TestDbInitializerSeedsEmptyDatabaseWithExampleData()
    {
        
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        
        bool isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.True(isDatabaseEmpty);
        
        DbInitializer.SeedDatabase(context);
        
        isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.False(isDatabaseEmpty);
        
        HashSet<Author> authors = context.Authors.ToHashSet();
        Assert.True(authors.Count == 12);
        Assert.Contains(authors, author => author.Name == "Roger Histand");
        Assert.Contains(authors, author => author.EmailAddress == "Mellie+Yost@ku.dk");
        Assert.Contains(authors, author => author.Name == "Adrian");
        
        HashSet<Cheep> cheeps = context.Cheeps.ToHashSet();
        Assert.True(cheeps.Count == 657);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 1);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 453);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 657);
        
        Assert.Contains(cheeps, cheep => cheep.Text == "In the first watch, and every creditor paid in full.");
        Assert.Contains(cheeps, cheep => cheep.Text == "It is he, then?");
    }
    
    
    
    //test that if there is a database, it is not overwritten - meaning that the data from the database is reachable and the data from the dbinitializer is not inserted
}