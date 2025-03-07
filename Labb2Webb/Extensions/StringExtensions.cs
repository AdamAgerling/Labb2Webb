using System.Text;

namespace Labb2Webb.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveExtraSpaces(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            string s = input.Trim();
            StringBuilder sb = new();

            for (int i = 0; i < s.Length; i++)
            {
                if (i < s.Length - 1 && char.IsWhiteSpace(s[i]) && char.IsWhiteSpace(s[i + 1]))
                {
                    continue;
                }
                sb.Append(s[i]);
            }
            return sb.ToString();
        }
    }
}
