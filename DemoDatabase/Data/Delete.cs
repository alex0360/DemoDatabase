namespace DemoDatabase.Data
{
    public class Delete
    {
        public string Command(string table, string key, int id) =>
            $"DELETE FROM {table} WHERE {key} = {id}";
    }
}