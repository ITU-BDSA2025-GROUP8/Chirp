using System.Data.Common;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Tests.Data;

public class DbInitializerTest : IDisposable
{
    //the set-up is the same as in Chirp/tests/Chirp.Infrastructure.Tests/Repositories/AuthorRepositoryTest.cs
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
        Assert.Equal(12, authors.Count);
        Assert.Contains(authors, author => author.Name == "Roger Histand");
        Assert.Contains(authors, author => author.EmailAddress == "Mellie+Yost@ku.dk");
        Assert.Contains(authors, author => author.Name == "Adrian");
        
        HashSet<Cheep> cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(657, cheeps.Count);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 1);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 453);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 657);
        
        Assert.Contains(cheeps, cheep => cheep.Text == "In the first watch, and every creditor paid in full.");
        Assert.Contains(cheeps, cheep => cheep.Text == "It is he, then?");
    }

    [Fact]
    public void TestDbInitializerDoesNotSeedDatabaseIfNotEmpty()
    {
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.True(isDatabaseEmpty);
        
        var b1 = new Author() { AuthorId = 7341, Name = "Test user", EmailAddress = "testuser@itu.dk", Cheeps = new List<Cheep>() };
        var b2 = new Author() { AuthorId = 9999, Name = "Example person", EmailAddress = "exampleperson@itu.dk", Cheeps = new List<Cheep>() };
        var authorsList = new List<Author>() { b1, b2 };
        
        var d1 = new Cheep() { CheepId = 7341, AuthorId = b1.AuthorId, Author = b1, Text = "I want to test if the database will not get the seeded data.", Date = DateTime.Parse("2025-08-01 13:13:13") };
        var d2 = new Cheep() { CheepId = 9999, AuthorId = b2.AuthorId, Author = b2, Text = "The database contain these cheeps instead.", Date = DateTime.Parse("2025-08-01 14:14:14") };
        
        var cheepsList = new List<Cheep>() { d1, d2 };
        b1.Cheeps = new List<Cheep>() { d1 };
        b2.Cheeps = new List<Cheep>() { d2 };
        
        context.Authors.AddRange(authorsList);
        context.Cheeps.AddRange(cheepsList);
        context.SaveChanges();
        
        isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.False(isDatabaseEmpty);
        HashSet<Author> authors = context.Authors.ToHashSet();
        Assert.Equal(2, authors.Count);
        HashSet<Cheep> cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(2, cheeps.Count);
        
        DbInitializer.SeedDatabase(context);
        
        authors = context.Authors.ToHashSet();
        Assert.Equal(2, authors.Count);
        Assert.DoesNotContain(authors, author => author.Name == "Roger Histand");
        Assert.DoesNotContain(authors, author => author.EmailAddress == "Mellie+Yost@ku.dk");
        Assert.DoesNotContain(authors, author => author.Name == "Adrian");
        
        cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(2, cheeps.Count);
        Assert.DoesNotContain(cheeps, cheep => cheep.CheepId == 1);
        Assert.DoesNotContain(cheeps, cheep => cheep.CheepId == 453);
        Assert.DoesNotContain(cheeps, cheep => cheep.CheepId == 657);
        
        Assert.DoesNotContain(cheeps, cheep => cheep.Text == "In the first watch, and every creditor paid in full.");
        Assert.DoesNotContain(cheeps, cheep => cheep.Text == "It is he, then?");
        
    }

    [Fact]
    public void TestDbInitializerDoesNotDoubleSeededDataIfCalledTwice()
    {
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        bool isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.True(isDatabaseEmpty);
        
        DbInitializer.SeedDatabase(context);
        
        isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.False(isDatabaseEmpty);
        HashSet<Author> authors = context.Authors.ToHashSet();
        Assert.Equal(12, authors.Count);
        HashSet<Cheep> cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(657, cheeps.Count);
        
        DbInitializer.SeedDatabase(context);
        authors = context.Authors.ToHashSet();
        Assert.Equal(12, authors.Count);
        cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(657, cheeps.Count);
        
    }
}