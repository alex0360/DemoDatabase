namespace DemoDatabase.Data
{
    public class Select
    {
        public string All(string table) =>
            $"SELECT * FROM {table}";
    }
}