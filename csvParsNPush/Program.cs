namespace csvParsNPush;

public class Program
{
    public static void Main(string[] args)
    {
        UserInputHelper inputHelper = new UserInputHelper();
        
        string csvPath = inputHelper.GetCsvFilePath();
        
        string connectionString = "Server=yourServer;Database=yourDB;User Id=your user id ;Password=your password;";
        string tableName = "your table name ";
        int countOfFields = 5; //insert count of fields in your csv file

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