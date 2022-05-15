using DemoDatabase.Extensions;
using NUnit.Framework;
using System;
using TestDataBase.Local;
using static DemoDatabase.Local.Settings;

namespace TestDataBase
{
    /// <summary>
    /// Descripción resumida de UnitTest2
    /// </summary>
    public class TestSelectSqlLite
    {
        private static readonly Logger _logger = Logger.GetInstance(Settings.Configuration);
        private static readonly DateTime dateTime = Convert.ToDateTime("2023-02-09T01:00:50");

        [Test]
        public void TestConsultLinq()
        {
            _logger.Insert.Information("Iniciando");

            _logger.Insert.Information(dateTime.ToString());

            var result = $"En {dateTime.RealTimeUntilNow()}";

            _logger.Insert.Information("Finalizado");

            Assert.Equals(result, null);
        }
    }
}