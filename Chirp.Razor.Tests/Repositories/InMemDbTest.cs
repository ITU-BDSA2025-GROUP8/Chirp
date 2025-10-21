using Chirp.Razor.DataModel;
using Chirp.Razor.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Test.Repositories;

[Collection("sqlite")] // use the fixture registered in SharedInMemoryFixture.cs
public class InMemDbTest
{
    private readonly SharedInMemoryFixture _fx;
    public InMemDbTest(SharedInMemoryFixture fx) => _fx = fx;

    [Fact]
    public async Task Insert_and_query_author_works()
    {
        // Arrange
        await using (var w = _fx.CreateContext())
        {
            w.Authors.Add(new Author { Name = "Bob", EmailAddress = "bobb@itu.dk" });
            await w.SaveChangesAsync();
        }

        // Act
        await using var r = _fx.CreateContext();
        var got = await r.Authors.SingleOrDefaultAsync(a => a.EmailAddress == "bobb@itu.dk");

        // Assert
        Assert.NotNull(got);
        Assert.Equal("Bob", got!.Name);
    }
}