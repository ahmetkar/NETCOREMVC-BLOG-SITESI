using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Blog.Service.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
                return "";
                
            string str = RemoveDiacritics(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    // Special cases for Turkish characters
                    if (c == 'ı') stringBuilder.Append('i');
                    else if (c == 'I' || c == 'İ') stringBuilder.Append('i');
                    else if (c == 'ğ' || c == 'Ğ') stringBuilder.Append('g');
                    else if (c == 'ü' || c == 'Ü') stringBuilder.Append('u');
                    else if (c == 'ş' || c == 'Ş') stringBuilder.Append('s');
                    else if (c == 'ö' || c == 'Ö') stringBuilder.Append('o');
                    else if (c == 'ç' || c == 'Ç') stringBuilder.Append('c');
                    else stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
