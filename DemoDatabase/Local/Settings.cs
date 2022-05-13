using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace DemoDatabase.Local
{
    public class Settings
    {
        public static IConfiguration Configuration { get; private set; }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                        .Build();
        }

        public class Logger
        {
            private static Logger _instance = null;
            private static readonly Object _mutex = new Object();

            public ILogger Insert { get; private set; }

            private Logger(IConfiguration configuration)
            {
                Insert = GetLogger(configuration);
            }

            private static ILogger GetLogger(IConfiguration configuration)
            {
                try
                {
                    Configuration = configuration ?? GetConfiguration();

                    return new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        .CreateLogger();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            public static Logger GetInstance(IConfiguration configuration = null)
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        _instance = new Logger(configuration);
                    }
                }

                return _instance;
            }
        }
    }
}