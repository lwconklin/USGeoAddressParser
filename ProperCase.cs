using System.Globalization;
using System.Text.RegularExpressions;

namespace AddressParser {
    static class ProperCase {

        private static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        private static string output = string.Empty;
        private const string Space = " ";

        public static string FormatProperCase(string value) {
            value = ProcessWhitespace(value);
            if (!IsAllUpperOrAllLower(value)) { // leave the CamelCase or Propercase names alone
                return value;
            } else {
                return ProcessWhitespace(textInfo.ToTitleCase(value.ToLower())); // if all upper case titlecase leaves as is
            }
        }

        private static string ProcessWhitespace(string value) {
            value = value.Trim().TrimStart().TrimEnd();
            value = Regex.Replace(value, @"\s+", Space);
            return value;
        }

        private static bool IsAllUpperOrAllLower(string input) {
            return (input.ToLower().Equals(input) || input.ToUpper().Equals(input));
        }
    } // class
} // namespace
