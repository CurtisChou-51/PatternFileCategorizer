using System.Text.RegularExpressions;

namespace PatternFileCategorizer.FileMatcher
{
    public static class MatcherUtils
    {
        public static string Normalize(string value)
        {
            return Regex.Replace(value, "[-.]", "").ToUpper();
        }

        public static IEnumerable<string> ExtractMatches(string text, List<Regex> regexes)
        {
            var results = new HashSet<string>();
            foreach (var regex in regexes)
            {
                foreach (Match m in regex.Matches(text))
                {
                    results.Add(Normalize(m.Value));
                }
            }
            return results;
        }
    }
}
