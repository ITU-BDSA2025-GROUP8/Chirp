namespace Chirp.Razor;
using Microsoft.Data.Sqlite;

public class DBFacade
{
    //The location of the database, which is currently stored locally
    private static readonly string sqlDBFilePath = GetDatabasePath();

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
                string author = GetAuthorName(Convert.ToInt32(values[1]));
                string message = (string) values[2];
                string timestamp = UnixTimeStampToDateTimeString(Convert.ToDouble(values[3]));
                
                //creates a new CheepViewModel
                list.Add(new CheepViewModel(author,message,timestamp));
                // See https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader.getvalues?view=dotnet-plat-ext-8.0
                // for documentation on how to retrieve complete columns from query results
            }

            reader.Close();
            connection.Close();
            return list;
        }
    }

    //Method that returns a list of Cheeps written by a specific author 
    public static List<CheepViewModel> ReadAuthor(string author, int? limit = null, int? offset = null)
    {
        
        List<CheepViewModel> list = new();
        
        
        var sqlQuery = @"SELECT * FROM message WHERE author_id = @authorId ORDER by message.pub_date desc"; //creates a query which get the data from the database
        
        //creates the connection to the database 
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open(); //opens the connection between the database and the program

            //sets the query as a command
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.Parameters.AddWithValue("@authorId", GetAuthorId(author));

            //reads through the database, retrieves data based on the query
            using var reader = command.ExecuteReader(); //var = SqlDataReader
            while (reader.Read())
            {
                //reads a whole row into the values array at a time, then prints them with their coloumn names
                Object[] values = new Object[reader.FieldCount];

                reader.GetValues(values);
                
                //assigns the column values to author, message and timestamp
                string message = (string) values[2];
                string timestamp = UnixTimeStampToDateTimeString(Convert.ToDouble(values[3]));
                
                //creates a new CheepViewModel
                list.Add(new CheepViewModel(author,message,timestamp));
                // See https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader.getvalues?view=dotnet-plat-ext-8.0
                // for documentation on how to retrieve complete columns from query results
            }
            
            reader.Close();
            connection.Close();
            return list;
        }
    }
    
    //method to convert id to author name
    private static string GetAuthorName(int userId)
    {
        string author = "";
        var sqlQuery = @"SELECT username FROM user WHERE user_id = @userId"; //query deciding to retrieve username based on user id
        
        //creates a connection, gets the author name 
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.Parameters.AddWithValue("@userId", userId);
            using var reader = command.ExecuteReader();
            reader.Read();
            author = reader.GetString(0);
            reader.Close();
            connection.Close();
            
        }
        return author;
    }
    
    //Method to return userID from the SQLite database based on username
    private static int GetAuthorId(string userName)
    {
        int authorId = -1; //default value which will never be an actual ID. Set to -1 for error-handling and testing
        var sqlQuery = @"SELECT user_id FROM user WHERE username = @username"; //query retrieving the user_id based on the userName
        
        //creates a connection, gets the user ID
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.Parameters.AddWithValue("@username", userName);
            using var reader = command.ExecuteReader();
            reader.Read();
            authorId = reader.GetInt32(0);
            reader.Close();
            connection.Close();
        }
        
        return authorId;
    }
    
    
    //Coverts the Unix Time stamp to a Date time formatted 'month/day/year hour:minutes:seconds'
    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM'/'dd'/'yy H':'mm':'ss");
    }

    //Returns the environment variable if possible, otherwise it returns a backup path
    private static string GetDatabasePath()
    {
        string? environment = Environment.GetEnvironmentVariable("CHIRPDBPATH"); //retrieves the environment variable locally
        
        //if it is not null or a whitespace, it returns the environment variable
        if (!string.IsNullOrWhiteSpace(environment))
        {
            return environment;
        }

        //otherwise it returns an already defined backup path
        string tmpPath = "/tmp/chirp.db";
        return tmpPath;
    }
    
}