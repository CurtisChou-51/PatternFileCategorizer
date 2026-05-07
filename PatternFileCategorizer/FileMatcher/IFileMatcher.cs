using System.Text.RegularExpressions;

namespace PatternFileCategorizer.FileMatcher
{
    public interface IFileMatcher
    {
        IEnumerable<string> GetMatches(string filePath, List<Regex> regexes);
    }
}
