using CommandLine;
using Database;

namespace Chirp.CLI.Client
{

    //The arguments that can be used with the program
    //For the shortName use "-" and for the longName use "--"
    public class Options
    {
        [Option('r', "read", Required = false, HelpText = "reads cheeps")]
        public bool Read { get; set; }
        [Option('c', "cheep", Required = false, HelpText = "writes cheeps")]
        public string Cheep { get; set; }
    }

    class Program
    {
        
        static void Main(string[] args)
        {

            WebApp.RunWeb();

            // if argument is in optings it runs the app, else it error handles
            //Parser.Default.ParseArguments<Options>(args)
            //.WithParsed(RunApp)
            //.WithNotParsed(HandleErrors);
        }

        static void RunApp(Options opt)
        {
            // Initialize database
            var database = new CSVDatabase<Cheep>();
            // check if the command used is read of cheep
            if (opt.Read)
            {
                try
                {
                    // Print all cheeps
                    UserInterface.UserInterface.printCheeps(database.Read()); //todo: should call localhost:5000/cheeps
                    // get the JSON content and unpack it so it can be printed, should be done in the UserInterface class
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            else if (opt.Cheep.Length !=0)
            {
                try
                {
                    long time = DateTimeOffset.Now.ToUnixTimeSeconds();
                    Cheep cheep = new Cheep { Author = Environment.UserName, Message = opt.Cheep, Timestamp = time };
                    database.Store(cheep);
                    //Make it to a JSON object that can be sendt as a push to localhost:5000/cheep instead of database.store
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        static void HandleErrors(IEnumerable<Error> errs)
        {
            Console.WriteLine("Failed to parse arguments");
        }
    }   
}