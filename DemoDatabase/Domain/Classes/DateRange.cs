using DemoDatabase.Domain.Message;
using DemoDatabase.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DemoDatabase.Domain.Classes
{
    public class DateRange : IDisposable
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DateRange(DateTime date)
        {
            StartDate = date;
            EndDate = date;
        }

        public DateRange(DateTime startDate, DateTime endDate)
        {
                if(startDate > endDate)
                {
                    throw new FormatException(Warnings.InvalidDateRange(startDate, endDate));
                }
                else
                {
                    StartDate = startDate;
                    EndDate = endDate;
                }
        }

        public DateRange(string startDate, string endDate, string expectedFormat = "yyyy-MM-dd")
        {
            string[] dateFormats = new string[]
            {
                "yyyyMM",
                "yyyy-MM",
                "yyyyMMdd",
                "yyyy-MM-dd",
                "yyMM",
                "yy-MM",
                "yyMMdd",
                "yy-MM-dd"
            };

            if(!DateTime.TryParseExact(startDate, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime convertedStartDate))
            {
                throw new FormatException(Warnings.ParameterFormatInvalid("La fecha", expectedFormat));
            }

            if(!DateTime.TryParseExact(endDate, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime convertedEndDate))
            {
                throw new FormatException(Warnings.ParameterFormatInvalid("La fecha", expectedFormat));
            }
            if(convertedStartDate > convertedEndDate)
            {
                Warnings.InvalidDateRange(convertedStartDate, convertedEndDate);
            }

            StartDate = convertedStartDate;
            EndDate = convertedEndDate;
        }

        public DateRange(string date, string expectedFormat = "yyyy-MM-dd")
        {
            string[] dateFormats = new string[]
            {
                "yyyyMM",
                "yyyy-MM",
                "yyyyMMdd",
                "yyyy-MM-dd",
                "yyMM",
                "yy-MM",
                "yyMMdd",
                "yy-MM-dd"
            };

            if(!DateTime.TryParseExact(date, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime convertedDate))
            {
                throw new FormatException(Warnings.ParameterFormatInvalid("La fecha", expectedFormat));
            }

            StartDate = convertedDate;
            EndDate = convertedDate;
        }

        public IEnumerable<DateTime> Iterate(DateInterval dateInterval = DateInterval.Daily)
        {
            for(var day = StartDate.Date; day <= EndDate.Date; day = day.AddDays(1))
            {
                switch(dateInterval)
                {
                    case DateInterval.Daily:

                    yield return day;

                    break;

                    case DateInterval.EndOfMonth:

                    if(day.Day.Equals(new DateTime(EndDate.Year, EndDate.Month, 1).AddMonths(1).AddDays(-1).Day))
                    {
                        yield return day;
                    }

                    break;
                }
            }
        }

        public void MoveStartDateToBeginMonth()
        {
            StartDate = new DateTime(StartDate.Year, StartDate.Month, 1);
        }

        public void MoveEndDateToEndMonth()
        {
            EndDate = new DateTime(EndDate.Year, EndDate.Month, DateTime.DaysInMonth(EndDate.Year, EndDate.Month));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}