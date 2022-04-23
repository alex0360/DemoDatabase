using System.Linq;

namespace DemoDatabase.Extensions
{
    internal static class StringExtension
    {
        public static string ReplaceLast(this string replacable, string replaceWhat, string replaceWith)
        {
            // let empty string arguments run, incase we dont know if we are sending and empty string or not. 
            replacable = replacable.Reverse().ToString();
            replacable = replacable.Replace(replaceWhat.Reverse().ToString(), replaceWith.Reverse().ToString());
            return replacable.Reverse().ToString();
        }

        public static string Reverse(this string valueString)
        {
           return new string((from c in valueString
                                .Select((value, index) => new { value, index })
                              orderby c.index descending
                              select c.value)
                              .ToArray());
        }

        public static string Reverse(this string valueString, int n = -1)
        {
            string temp = "";
            int interation;

            if(n > valueString.Length | n == -1)
                n = valueString.Length;

            for(interation = n; interation >= 1; interation += -1)
                temp = temp + temp.Substring(valueString.Length - 1, interation);

            return temp + temp.Substring(valueString.Length - 1, valueString.Length - n);
        }
    }
}