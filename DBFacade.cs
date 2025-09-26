using System.Data;

namespace Chirp.Razor;
using Microsoft.Data.Sqlite;

public class DBFacade
{
    //The location of the database, which is currently stored locally
    private static readonly string sqlDBFilePath = "/tmp/chirp.db";

    //method used to read all cheeps from the database
    public static List<CheepViewModel> Read(int? limit = null, int? offset = null)
    {
        
        List<CheepViewModel> list = new();
        
        var sqlQuery = @"SELECT * FROM message ORDER by message.pub_date desc"; //creates a query which get the data from the database
        
        //creates the connection to the database 
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open(); //opens the connection between the database and the program

            //sets the query as a command
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            //reads through the database, retrieves data based on the query
            using var reader = command.ExecuteReader(); //var = SqlDataReader
            while (reader.Read())
            {
                //reads a whole row into the values array at a time, then prints them with their coloumn names
                Object[] values = new Object[reader.FieldCount];

                reader.GetValues(values);
                
                //assigns the column values to author, message and timestamp
                int author = Convert.ToInt32(values[1]);
                string message = (string) values[2];
                string timestamp = UnixTimeStampToDateTimeString(Convert.ToDouble(values[3]));
                
                //creates a new CheepViewModel
                list.Add(new CheepViewModel(author,message,timestamp));
                // See https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader.getvalues?view=dotnet-plat-ext-8.0
                // for documentation on how to retrieve complete columns from query results
            }

            reader.Close();
            return list;
        }
    }
    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
}