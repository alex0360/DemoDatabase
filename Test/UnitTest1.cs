using DemoDatabase.Controllers;
using NUnit.Framework;
using Test.Local;
using static DemoDatabase.Local.Settings;

namespace Test
{
    public class Tests
    {
        private static readonly Logger _logger = Logger.GetInstance(Settings.Configuration);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            _logger.Insert.Information("Iniciando");

            Client client = new Client();

            var result = client.ConsultLinq();

            client.Show(result);

            _logger.Insert.Information("Finalizado");

            Assert.IsNotNull(result);
        }
    }
}