using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardDictionaryBot.Utils
{
    public static class StringExtensions
    {
        public static string[] Words(this String str)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = str.Split(delimiterChars);
            return words;
        }

        public static string FirstWord(this string str)
        {
            return str.Words()[0];
        }

        public static string WordAtPosition(this string str, int pos)
        {
            return str.Words()[pos - 1];
        }

    }
}