using DemoDatabase.Domain.Classes;
using DemoDatabase.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace TestDataBase
{
    [TestClass]
    public class UnitTestDateRange
    {
        [TestMethod]
        public void TestDateRangeConstructor()
        {
            DateTime startDate = new DateTime(2014, 12, 25);
            DateTime endDate = new DateTime(2013, 8, 13);

            try
            {
                DateRange exampleDate = new DateRange(startDate, endDate);
            }
            catch(FormatException exception)
            {

                Assert.AreEqual(exception.Message, $"Rango de fechas inválido. La fecha inicial: {startDate:yyyy-MM-dd} no puede ser mayor a la final: {endDate:yyyy-MM-dd}");
            }
        }

        [TestMethod]
        public void TestDateRangeFormatConstructor()
        {
            var startDate = "2018-03-15";
            var endDate = "2013-08-13";
            var expectedFormat = "yyyy-MM-dd";

            var dateRange = new DateRange(startDate, endDate, expectedFormat);

            var result = DateTime.ParseExact(startDate, expectedFormat, CultureInfo.InvariantCulture);

            Assert.AreEqual(result, dateRange.StartDate);
        }

        [TestMethod]
        public void TestIterate()
        {
            var startDate = new DateTime(2014, 12, 25);
            var endDate = new DateTime(2018, 8, 13);

            var date = new DateRange(startDate, endDate);

            var x = date.Iterate().GetEnumerator();
        }

        [TestMethod]
        public void TestDateFormatContructor()
        {
            var date = "201905";
            var dateRange = new DateRange(date, "yyyyMM");

            var dateTime = DateTime.ParseExact(date, "yyyyMM", CultureInfo.InvariantCulture);

            Assert.AreEqual(dateTime, dateRange.StartDate);
        }

        [TestMethod]
        public void TestDateRangeIterate()
        {
            var startDate = new DateTime(2022, 04, 05);
            var endDate = new DateTime(2022, 04, 07);

            var dateTimes = new List<DateTime>
            {
                new DateTime(2022, 04, 05),
                new DateTime(2022, 04, 06),
                new DateTime(2022, 04, 07)
            };

            var dateRange = new DateRange(startDate, endDate);

            var date = dateRange.Iterate().ToList();

            var result = string.Join(", ", date);

            Assert.AreEqual(dateTimes, date);
        }

        [TestMethod]
        public void TestDateRangeIterateByDays()
        {
            var startDate = new DateTime(2022, 04, 05);
            var endDate = new DateTime(2022, 04, 07);

            var dateTimes = new List<DateTime>
            {
                new DateTime(2022, 04, 05),
                new DateTime(2022, 04, 06),
                new DateTime(2022, 04, 07)
            };

            var dateRange = new DateRange(startDate, endDate);

            var date = dateRange.Iterate((DateInterval)0).ToList();

            Assert.AreEqual(dateTimes, date);
        }

        [TestMethod]
        public void TestDateRangeIterateByEndOfMonth()
        {
            var startDate = new DateTime(2022, 04, 05);
            var endDate = new DateTime(2022, 06, 07);

            var dateTimes = new List<DateTime>
            {
                new DateTime(2022, 04, 30),
                new DateTime(2022, 05, 30)
            };

            var dateRange = new DateRange(startDate, endDate);

            var date = dateRange.Iterate((DateInterval)1).ToList();

            Assert.AreEqual(dateTimes, date);
        }

        [TestMethod]
        public void TestMoveStartDateToBeginMonth()
        {
            var expected = new DateTime(2019, 10, 1);
            var date = new DateTime(2019, 10, 17);

            var dateRange = new DateRange(date);

            dateRange.MoveStartDateToBeginMonth();

            Assert.AreEqual(expected, dateRange.StartDate);
        }

        [TestMethod]
        public void TestMoveEndDateToEndMonth()
        {
            var expected = new DateTime(2019, 09, 30);
            var date = new DateTime(2019, 09, 17);

            var dateRange = new DateRange(date);

            dateRange.MoveEndDateToEndMonth();

            Assert.AreEqual(expected, dateRange.EndDate);
        }
    }
}