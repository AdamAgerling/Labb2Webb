namespace BlazorFrontend.Extensions
{
    public class StringExtensions
    {
        public static string FormatUserName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return "";
            }
            var parts = fullName.Split(' ');
            if (parts.Length == 1)
            {
                return parts[0];
            }
            return $"{parts[0]} {parts[1].Substring(0, 1)}.";
        }
    }
}
