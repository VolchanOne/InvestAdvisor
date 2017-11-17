using System.Text.RegularExpressions;

namespace InvestAdvisor.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNonAlphaNumericChars(this string str)
        {
            return Regex.Replace(str, @"[^A-Za-z0-9]+", string.Empty);
        }
    }
}
