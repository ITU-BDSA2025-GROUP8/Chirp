namespace SimpleDB;

using System.Globalization;
using CsvHelper;

public sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
    // Reads all records from CSV-file using CsvHelper
    public IEnumerable<T> Read(int? limit = null)
    {
        using (var reader = new StreamReader("chirp_cli_db.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }

    //Adds a new Cheep to a specified csv file 
    public void Store(T record)
    {
        using var writer = new StreamWriter("chirp_cli_db.csv", true);
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
                csv.WriteRecord(record);
                csv.NextRecord();
                Console.WriteLine("Cheeped: " + record);
        }
    }
}