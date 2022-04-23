using System;
using System.Text;

namespace DemoDatabase.Extensions
{
    internal static class DateTimeExtension
    {
        private static int year;
        private static int month;
        private static int day;
        private static int hour;
        private static int minute;
        private static int second;

        private static Int16 segmentsAdded = 0;

        private static byte _allowSegments;

        public static string ElapsedTime(this DateTime dtEvent)
        {
            TimeSpan TS = DateTime.Now - dtEvent;

            int intYears = TS.Days / 365;
            int intMonths = TS.Days / 30;
            int intDays = TS.Days;
            int intHours = TS.Hours;
            int intMinutes = TS.Minutes;
            int intSeconds = TS.Seconds;

            if(intYears > 0) return String.Format("En {0} {1}", intYears, (intYears == 1) ? "año" : "años");
            else if(intMonths > 0) return String.Format("En {0} {1}", intMonths, (intMonths == 1) ? "mes" : "meses");
            else if(intDays > 0) return String.Format("En {0} {1}", intDays, (intDays == 1) ? "día" : "días");
            else if(intHours > 0) return String.Format("En {0} {1}", intHours, (intHours == 1) ? "hora" : "horas");
            else if(intMinutes > 0) return String.Format("En {0} {1}", intMinutes, (intMinutes == 1) ? "minuto" : "minutos");
            else if(intSeconds > 0) return String.Format("En {0} {1}", intSeconds, (intSeconds == 1) ? "segundo" : "segundos");
            else
            {
                return String.Format("En {0} a las {1}", dtEvent.ToShortDateString(), dtEvent.ToShortTimeString());
            }
        }

        public static string RealTimeUntilNow(this DateTime dateTime, byte allowSegments = 2)
        {
            // bAllowSegments identifies how many segments to show... ie: if 3, then return string would be (as an example)...
            // "3 years, 2 months and 13 days" the top 3 time categories are returned, if bAllowSegments is 2 it would return
            // "3 years and 2 months" and if 6 (maximum value) would return "3 years, 2 months, 13 days, 13 hours, 29 minutes and 9 seconds"

            _allowSegments = allowSegments;

            var dateTimeNow = DateTime.Now;
            var daysInBaseMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            year = dateTimeNow.Year - dateTime.Year;
            month = dateTimeNow.Month - dateTime.Month;

            if(month < 0)
            {
                month += 12; year -= 1;
            }
            // add 1 year to months, and remove 1 year from years.

            day = dateTimeNow.Day - dateTime.Day;

            if(day < 0)
            {
                day += daysInBaseMonth; month -= 1;
            }

            hour = dateTimeNow.Hour - dateTime.Hour;

            if(hour < 0)
            {
                hour += 24; day -= 1;
            }

            minute = dateTimeNow.Minute - dateTime.Minute;

            if(minute < 0)
            {
                minute += 60; hour -= 1;
            }

            second = dateTimeNow.Second - dateTime.Second;

            if(second < 0)
            {
                second += 60; minute -= 1;
            }

            // if the string is entirely empty, that means it was just posted so its less than a second ago, and an empty string getting passed will cause an error
            // so we construct our own meaningful string which will still fit into the "Posted * ago " syntax...

            var message = MessageBuild();

            if(message.ToString() == "")
                message.Insert(message.Length, "menos de 1 segundo");

            if(allowSegments > 2)
                return message.ToString().TrimEnd(' ', ',').ToString();

            return message.ToString().TrimEnd(' ', ',').ToString().ReplaceLast(",", " y");
        }

        private static string MessageBuild()
        {
            #region this is the display functionality

            StringBuilder message = new StringBuilder();

            if(year > 0)
            {
                message.AddSegments(year, "año");
            }

            if(_allowSegments == segmentsAdded)
                return message.ToString();

            if(month > 0)
            {
                message.Append(month + " mes");

                message.Append((month != 1 ? "es" : "") + ", ");

                segmentsAdded += 1;
            }

            if(_allowSegments == segmentsAdded)
                return message.ToString();

            if(day > 0)
            {
                message.AddSegments(day, "día");
            }

            if(_allowSegments == segmentsAdded)
                return message.ToString();

            if(hour > 0)
            {
                message.AddSegments(hour, "hora");
            }

            if(_allowSegments == segmentsAdded)
                return message.ToString();

            if(minute > 0)
            {
                message.AddSegments(minute, "minuto");
            }

            if(_allowSegments == segmentsAdded)
                return message.ToString();

            if(second > 0)
            {
                message.AddSegments(second, "segundo");
            }

            return message.ToString();
            #endregion
        }

        private static string AddSegments(this StringBuilder stringBuilder, int value, string key)
        {
            stringBuilder.Append(value + $" {key}");

            stringBuilder.Append(((value != 1) ? "s" : "") + ", ");

            segmentsAdded += 1;

            return stringBuilder.ToString();
        }
    }
}