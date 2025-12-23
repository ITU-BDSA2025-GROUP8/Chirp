namespace Chirp.Core.Interfaces;

public interface IDbInitializer
{
    public Task SeedDatabase();
}