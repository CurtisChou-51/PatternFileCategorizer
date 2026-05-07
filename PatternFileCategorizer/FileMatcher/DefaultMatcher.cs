using System.Text.RegularExpressions;

namespace PatternFileCategorizer.FileMatcher
{
    public class DefaultMatcher : IFileMatcher
    {
        public IEnumerable<string> GetMatches(string filePath, List<Regex> regexes)
        {
            string fileName = Path.GetFileName(filePath);
            return MatcherUtils.ExtractMatches(fileName, regexes);
        }
    }
}
