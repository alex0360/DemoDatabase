using Microsoft.Extensions.Configuration;

namespace DemoDatabase.Local
{
    public static class Parameterize
    {
        public static string Path => Settings.Configuration.GetValue<string>("connection:sqlite:path");
        public static int Version => Settings.Configuration.GetValue<int>("connection:sqlite:version");
        public static string PasswordLite => Settings.Configuration.GetValue<string>("connection:sqlite:password");
        public static string DataSource => Settings.Configuration.GetValue<string>("connection:sqlserver:dataSource");
        public static string DataBase => Settings.Configuration.GetValue<string>("connection:sqlserver:dataBase");
        public static string UserId => Settings.Configuration.GetValue<string>("connection:sqlserver:userId");
        public static string PasswordServer => Settings.Configuration.GetValue<string>("connection:sqlserver:password");
    }
}