using System;

namespace DemoDatabase.Extensions
{
    internal static class DateTimeExstension
    {
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
                return String.Format("En {0} às {1}", dtEvent.ToShortDateString(), dtEvent.ToShortTimeString());
            }
        }
    }
}