using Database; //CSVDatabase<T>
using Microsoft.AspNetCore.Builder; //to not use full namespace when using 
using Microsoft.AspNetCore.Http;


namespace Chirp.CLI.Client;

public class WebApp
{
    public static void RunWeb()
    {
    var WebApplicationBuilder = WebApplication.CreateBuilder(new string [] {}); //Builder
    var application = WebApplicationBuilder.Build(); //Build

    var database = new CSVDatabase<Cheep>();
    
    application.MapGet("/cheeps", () =>
    {
        return database.Read();
    }); //when a GET request is made .Read

    application.MapPost("/cheep", async (HttpContext context) => //Get the http context/body
    {
        var input = await context.Request.ReadFromJsonAsync<Cheep>(); //todo: find out what it does
        long time = DateTimeOffset.Now.ToUnixTimeSeconds();
        Cheep cheep = new Cheep { Author = Environment.UserName, Message = input.Message, Timestamp = time };
        database.Store(cheep); //adds it to the database - hopefully?
    }); //appends JSON body to ??
    
    application.Run();
    }
}
