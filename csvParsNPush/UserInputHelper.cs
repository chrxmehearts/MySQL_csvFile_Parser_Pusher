namespace csvParsNPush;

public class UserInputHelper
{
    public string GetCsvFilePath()
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
}