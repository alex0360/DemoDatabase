namespace DemoDatabase.Data
{
    public class Connector
    {

        public string ConnectionString { get; set; }

        public Connector(string dataSource, int version, string password = null)
        {
            ConnectionString = (string.IsNullOrEmpty(password)) ?
                connectionString(dataSource, version) :
                connectionString(dataSource, version, password);
        }

        public Connector(string dataSource, string dataBase, string user, string password)
        {
            ConnectionString = connectionString(dataSource, dataBase, user, password);
        }

        // Data Source = c:\mydb.db;Version=3;
        private string connectionString(string dataSource, int version) => 
            $"Data Source={dataSource};Version={version};";

        // Data Source=c:\mydb.db;Version=3;Password=myPassword;
        private string connectionString(string dataSource, int version, string password) =>
            $"Data Source={dataSource};Version={version};Password={password};";

        // Data Source=myServerAddress; Initial Catalog=myDataBase; User Id =myUsername; Password=myPassword;
        private string connectionString(string dataSource, string dataBase, string user, string password) =>
            $"Data Source={dataSource}; Initial Catalog={dataBase}; User Id ={user}; Password={password}";
    }
}