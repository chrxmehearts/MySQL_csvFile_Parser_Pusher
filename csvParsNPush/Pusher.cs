using Dapper;
using MySql.Data.MySqlClient;

namespace csvParsNPush;

public class Pusher
{
    private readonly string connectionString;
    private readonly string tableName;

    public Pusher(string connectionString, string tableName)
    {
        this.connectionString = connectionString;
        this.tableName = tableName;
    }

    public void PushToDB(List<Dictionary<string, string?>> data)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        foreach (var record in data)
        {
            var columns = string.Join(", ", record.Keys);
            var values = string.Join(", ", record.Keys.Select(k => "@" + k));

            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
            Console.WriteLine($"Executing query: {query}"); 

            var parameters = new DynamicParameters();
            foreach (var field in record)
            {
                parameters.Add("@" + field.Key, field.Value);
            }

            connection.Execute(query, parameters, transaction);
        }

        transaction.Commit();
    }
}