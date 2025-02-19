using System.Globalization;
using CsvHelper;

namespace csvParsNPush;

public class Parser
{
    public List<Dictionary<string, string?>> ParseCsv(string csvPath, int countOfFields)
    {
        var records = new List<Dictionary<string, string?>>();
        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        var headers = new List<string>();
        for (int i = 0; i < countOfFields; i++)
        {
            headers.Add(csv.GetField(i) ?? $"Field {i + 1}");
        }

        while (csv.Read())
        {
            var record = new Dictionary<string, string?>();
            for (int i = 0; i < countOfFields; i++)
            {
                string header = headers[i];
                string? value = csv.GetField(i);
                
                record.Add(header, value);
            }

            records.Add(record);
        }

        return records;
    }
}