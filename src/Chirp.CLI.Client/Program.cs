using CommandLine;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;//for jsonSerializer
using System.Text;//for encoding

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
		[Option('w', "web", Required = false, HelpText = "runs web service")]
        public bool Web { get; set; }
    }

    class Program
    {
        
        static async Task Main(string[] args)
        {

            WebApp.RunWeb();

            //if argument is in optings it runs the app, else it error handles
            var result = Parser.Default.ParseArguments<Options>(args);
            await result.WithParsedAsync(RunApp); //async overload todo: why?
            result.WithNotParsed(HandleErrors);
        }
		//make http client
		private static HttpClient baseClient = new () 
		{
		BaseAddress = new Uri("http://localhost:5000"),
		};


        static async Task RunApp(Options opt)
        {
          
            if (opt.Read)
            {
                try
                {
				
					var cheeps = await baseClient.GetFromJsonAsync<List<Cheep>>("/cheeps") //gets response from GET request to localhost:5000/cheeps
								?? new List<Cheep>();//if nothing is returned from GET request
                    // Print all cheeps
                    UserInterface.UserInterface.printCheeps(cheeps);
					
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            else if ((opt.Cheep?.Length ?? 0) > 0) //check if is null and length > 0
            {
                try
                {
                    
                    long time = DateTimeOffset.Now.ToUnixTimeSeconds();
                    Cheep cheep = new Cheep { Author = Environment.UserName, Message = opt.Cheep, Timestamp = time };
                    //todo: make post request with body containing JSON with information { Author = Environment.UserName, Message = opt.Cheep, Timestamp = time}
                    using StringContent jsonBody = new(
                        JsonSerializer.Serialize(new
                        {
                            Author = Environment.UserName,
                            Message = opt.Cheep,
                            Timestamp = time
                        }),
                        Encoding.UTF8,
                        "application/json"); //format: we are serializing into JSON. First argument is the object to serialize, the other is text encoding (UTF8), the third makes header for http (media type)
                
                    //todo: might want to add response.EnsureSuccessStatusCode()
                    //write to console 

                    //todo: send post request to BaseAddress/cheep
                    using HttpResponseMessage response = await baseClient.PostAsync("cheep", jsonBody);

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