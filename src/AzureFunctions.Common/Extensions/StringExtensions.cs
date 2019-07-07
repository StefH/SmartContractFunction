using System;

namespace AzureFunctions.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comparisonType)
        {
            return source?.IndexOf(toCheck, comparisonType) >= 0;
        }
    }
}
