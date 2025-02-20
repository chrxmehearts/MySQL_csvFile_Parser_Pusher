namespace csvParsNPush;

public class Program
{
    public static void Main(string[] args)
    {
        UserInputHelper inputHelper = new UserInputHelper();
        
        string csvPath = inputHelper.GetCsvFilePath();
        
        //for ex: "Server=yourServer;Database=yourDB;User Id=your_user_id;Password=your_password;"
        string connectionString = inputHelper.GetConnectionString();
        
        string tableName = inputHelper.GetTableName(connectionString);
        
        int countOfFields = CsvHelper.GetFieldCountFromCsv(csvPath);

        Parser parser = new Parser();
        Pusher pusher = new Pusher(connectionString, tableName);

        List<Dictionary<string, string?>> data = parser.ParseCsv(csvPath, countOfFields);

        logOfParsedData(data);

        pusher.PushToDB(data);
    }
    
    
    
    private static void logOfParsedData(List<Dictionary<string, string?>> data)
    {
        Console.WriteLine("Parsed data:");
        foreach (var record in data)
        {
            Console.WriteLine("{" + string.Join(", ", record) + "}");
        }
    }
}