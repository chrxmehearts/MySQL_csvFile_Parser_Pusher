namespace csvParsNPush;

public class Program
{
    public static void Main(string[] args)
    {
        string csvPath = GetCsvFilePath();
        
        string connectionString = "Server=yourServer;Database=yourDB;User Id=your user id ;Password=your password;";
        string tableName = "your table name ";
        int countOfFields = 5; //insert count of fields in your csv file

        Parser parser = new Parser();
        Pusher pusher = new Pusher(connectionString, tableName);

        List<Dictionary<string, string?>> data = parser.ParseCsv(csvPath, countOfFields);

        logOfParsedData(data);

        pusher.PushToDB(data);
    }

    private static string GetCsvFilePath()
    {
        string? filePath;
        while (true)
        {
            Console.WriteLine("Please enter your csv file path:");
            filePath = Console.ReadLine();
            
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error: File path cannot be empty. Try again.");
                continue;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found. Please enter a valid file path.");
                continue;
            }

            if (Path.GetExtension(filePath) != ".csv")
            {
                Console.WriteLine("Error: The file must be a CSV. Try again.");
                continue;
            }

            break;
        }


        return filePath;
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