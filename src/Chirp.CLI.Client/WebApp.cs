var WebApplicationBuilder = WebApplication.CreateBuilder(args);
var application = WebApplicationBuilder.Build();

application.MapGet("/cheeps", () => CSVDatabase<Cheep>.Read());
application.MapPost("/cheep", (string Author, string Message, long Timestamp) => CSVDatabase<Cheep>.Store());

application.Run(); 

public record Cheep(string Author, string Message, long Timestamp)