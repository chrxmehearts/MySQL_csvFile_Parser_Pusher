namespace csvParsNPush;

using System;
using System.IO;

public class CsvHelper
{
    public static int GetFieldCountFromCsv(string filePath)
    {
        try
        {
            string firstLine = File.ReadLines(filePath).FirstOrDefault();
            
            if (string.IsNullOrEmpty(firstLine))
            {
                Console.WriteLine("Error: The file is empty.");
                return 0;
            }
            
            string[] fields = firstLine.Split(',');
            
            return fields.Length;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
            return 0;
        }
    }
}
