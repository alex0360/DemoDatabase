using System;

namespace DemoDatabase.Domain.Message
{
    internal class Warnings
    {
        /// <summary>
        ///Rango de fechas inválido. La fecha inicial: {start} no puede ser mayor a la final: {end}
        /// </summary>
        /// <param name="start">Start</param>
        /// <param name="end">End</param>
        /// <returns>Messages</returns>
        public static string InvalidDateRange(DateTime start, DateTime end)
        {
            return $"Rango de fechas inválido. La fecha inicial: {start:yyyy-MM-dd} no puede ser mayor a la final: {end:yyyy-MM-dd}";
        }

        /// <summary>
        ///{parameter} no tiene un formato válido, el formato esperado es: {format}
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <param name="format">Format</param>
        /// <returns>Messages</returns>
        public static string ParameterFormatInvalid(string parameter, string format)
        {
            return $"'{parameter}' no tiene un formato válido, el formato esperado es: '{format}'";
        }
    }
}