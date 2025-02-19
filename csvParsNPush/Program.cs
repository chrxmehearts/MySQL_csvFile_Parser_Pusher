namespace csvParsNPush;

public class Program
{
    public static void Main(string[] args)
    {
        string csvPath = "your csv path";
        string connectionString = "your mysql connection";
        int countOfFields = 5; //type count of fields in your csv file
        Parser parser = new Parser();
        Pusher pusher = new Pusher();
        
    }
}