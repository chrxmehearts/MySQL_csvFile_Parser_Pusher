using Dapper;
using MySql.Data.MySqlClient;

namespace csvParsNPush;

public class UserInputHelper
{
    public string GetTableName(string connectionString)
    {
        string? tableName;
        while (true)
        {
            Console.WriteLine("Please enter your MySQL table name:");
            tableName = Console.ReadLine();

            if (IsValidTableName(connectionString, tableName))
            {
                break;
            }
        }

        return tableName!;
    }

    private bool IsValidTableName(string connectionString, string? tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
        {
            Console.WriteLine("Error: Table name cannot be empty. Try again.");
            return false;
        }

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SHOW TABLES LIKE @TableName";
                var result = connection.QueryFirstOrDefault(query, new { TableName = tableName });
                if (result != null)
                {
                    Console.WriteLine($"Table '{tableName}' exists in the database.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: Table '{tableName}' does not exist in the database.");
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return false;
        }
    }


    public string GetConnectionString()
    {
        string? connectionString;
        while (true)
        {
            Console.WriteLine("Please enter your MySQL database connection string:");
            connectionString = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.WriteLine("Error: Connection string cannot be empty.");
                continue;
            }

            if (!TestDatabaseConnection(connectionString))
            {
                Console.WriteLine("Error: Invalid database connection string.");
                continue;
            }

            break;
        }

        return connectionString;
    }

    private bool TestDatabaseConnection(string connectionString)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Success Connection");
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: Failed to connect to the MySQL database. {e.Message}");
            return false;
        }
    }


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