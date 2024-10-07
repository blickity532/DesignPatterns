public class DBSingleton
{
    private static readonly object _lock = new object();
    private static volatile DBSingleton? _instance;

    public string ConnectionString
    {
        get; private set;
    }

    private DBSingleton(string connection) => ConnectionString = connection;

    public static DBSingleton GetInstance(string connection)
    {
        if (string.IsNullOrEmpty(connection))
        {
            throw new ArgumentException("Connection string cannot be null or empty.", nameof(connection));
        }

        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DBSingleton(connection);
                }
            }
        }
        return _instance;
    }


    public static void Main(string[] args)
    {
        // Example usage of the singleton
        DBSingleton instance = DBSingleton.GetInstance("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;");
        System.Console.WriteLine(instance.ConnectionString);
    }
}
