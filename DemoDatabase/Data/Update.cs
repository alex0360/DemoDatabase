namespace DemoDatabase.Data
{
    public class Update
    {
        public string Command(int id, string name, string address) =>
            $"UPDATE Cliente SET NAME = '{name}', ADDRESS = '{address}' WHERE ID = {id};";
    }
}