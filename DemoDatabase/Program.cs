using DemoDatabase.Extensions;
using System;
using static DemoDatabase.Local.Settings;

namespace DemoDatabase
{
    public class Program
    {
        private static readonly Logger _logger = Logger.GetInstance();

        private static readonly DateTime dateTime = Convert.ToDateTime("2023-02-09T01:00:50");

        public static void Main(string[] args)
        {
            _logger.Insert.Information("Iniciando")

            _logger.Insert.Information(dateTime.ToString());

            _logger.Insert.Information($"En {dateTime.RealTimeUntilNow()}");

            _logger.Insert.Information("Finalizado");

            Console.ReadKey();
        }
    }
}