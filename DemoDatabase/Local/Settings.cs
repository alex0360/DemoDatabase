using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace DemoDatabase.Local
{
    public class Settings
    {
        public static IConfiguration Configuration => new ConfigurationBuilder()
                        .SetBasePath("C:\\Users\\edelossantos\\source\\repos\\DemoDatabase\\DemoDatabase\\")
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                        .Build();

        public static class Parameterize
        {
            public static string Path => Configuration.GetValue<string>("connection:sqlite:path");
            public static int Version => Configuration.GetValue<int>("connection:sqlite:version");
            public static string PasswordLite => Configuration.GetValue<string>("connection:sqlite:password");
            public static string DataSource => Configuration.GetValue<string>("connection:sqlserver:dataSource");
            public static string DataBase => Configuration.GetValue<string>("connection:sqlserver:dataBase");
            public static string UserId => Configuration.GetValue<string>("connection:sqlserver:userId");
            public static string PasswordServer => Configuration.GetValue<string>("connection:sqlserver:password");
        }

        public class Logger
        {
            private static Logger _instance = null;
            private static readonly Object _mutex = new Object();
            public ILogger Insert { get; private set; }

            private Logger()
            {
                Insert = GetLogger();

            }

            private static ILogger GetLogger()
            {
                try
                {
                    return new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        .CreateLogger();

                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            public static Logger GetInstance()
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        _instance = new Logger();
                    }
                }

                return _instance;
            }
        }
    }
}
