using DemoDatabase.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestDataBase.Local;
using static DemoDatabase.Local.Settings;

namespace TestDataBase
{
    /// <summary>
    /// Descripción resumida de UnitTest2
    /// </summary>
    [TestClass]
    public class TestSelectSqlLite
    {
        private static readonly Logger _logger = Logger.GetInstance(Settings.Configuration);

        private static readonly DateTime dateTime = Convert.ToDateTime("2023-02-09T01:00:50");

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestConsultLinq()
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