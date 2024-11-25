using System.Globalization;

namespace ProductService.Helpers
{
    public static class StringHelper
    {
        public static string ToSentenceCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
