using Database; //CSVDatabase<T>
using Microsoft.AspNetCore.App //to not use full namespace when using 

namespace Chirp.CLI.Client;

public class webApp
{
    var WebApplicationBuilder = WebApplication.CreateBuilder(args); //Builder
    var application = WebApplicationBuilder.Build(); //Build

    application.MapGet("/cheeps", () => CSVDatabase<Cheep>.Read()); //when a GET request is made .Read

    application.MapPost("/cheep", ([FromBody] Cheep input) => )
    {
        long time = DateTimeOffset.Now.ToUnixTimeSeconds();
        Cheep cheep = new Cheep { Author = Environment.UserName, Message = input.Message, Timestamp = time };
        CSVDatabase<Cheep>.Store(cheep);
    }); //appends JSON body to ??
    
    application.Run();

}
