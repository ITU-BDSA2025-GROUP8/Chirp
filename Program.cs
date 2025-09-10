using Chirp.CLI.UserInterface;
using SimpleDB;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.CommandLine.Invocation;


namespace Chirp.CLI;
class Program
{
    static void Main(string[] args)
    {
        // Initialize database
        var database = new CSVDatabase<Cheep>();
        
        // Creating the root command - should automatically have a help and a version option and a suggets directive
        RootCommand rootCommand = new("Sample app for System.CommandLine"); //todo: skriv en anden description
       
        //Create subcommands and adds them to the root command. 
        var readCommand = new Command("read", "Reads current cheeps"); //todo: opdater potentielt beskrivelsen
        rootCommand.Add(readCommand);
        
        
        var cheepCommand = new Command("cheep", "create a new cheep");
        var messageArg = new Argument<string>("message");
        rootCommand.Add(cheepCommand);
        cheepCommand.Add(messageArg);
        
        // Command handlers
        cheepCommand.SetAction((ParseResult parseResult) =>
        {
            var message = parseResult.GetValue(messageArg);

            Console.WriteLine($"writes: {message} ({DateTime.Now})");
        });


        
        if (args[0] == "read")
        {
            try
            {
                // Print all cheeps
                UserInterface.UserInterface.printCheeps(database.Read());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        else if (args[0] == "cheep")
        {
            try
            {
                long time = DateTimeOffset.Now.ToUnixTimeSeconds();
                Cheep cheep = new Cheep { Author = Environment.UserName, Message = args[1], Timestamp = time };
                database.Store(cheep);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        else
        {
            Console.WriteLine("Command not recognized");
        }
    }
    
    
    
}