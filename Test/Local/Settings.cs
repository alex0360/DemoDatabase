using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Test.Local
{
    internal static class Settings
    {
        public static IConfiguration Configuration => GetConfiguration();

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                        .Build();
        }
    }
}