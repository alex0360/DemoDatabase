using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DemoDatabase.Infrastructure.extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// change the letters with accents in an entry
        /// </summary>
        /// <param name="value">the string to which the characters should be changed.</param>
        /// <returns>The altered text if there are characters with matching.</returns>
        public static string ChangingLettersWithAccents(this String value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return new String(
                value.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()
            )
            .Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Strip unwanted characters and replace them with empty string
        /// </summary>
        /// <param name="value">the string to strip characters from.</param>
        /// <param name="textToStrip">Characters to strip. Can contain Regular expressions</param>
        /// <returns>The stripped text if there are matching string.</returns>
        /// <remarks>If error occurred, original text will be the output.</remarks>
        public static string Strip(this string value, string textToStrip)
        {
            var stripText = value;

            if (string.IsNullOrEmpty(value)) return stripText;

            try
            {
                stripText = Regex.Replace(value, textToStrip, string.Empty);
            }
            catch
            {
            }
            return stripText;
        }
    }
}