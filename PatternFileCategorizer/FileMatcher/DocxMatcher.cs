using DocumentFormat.OpenXml.Packaging;
using System.Text.RegularExpressions;

namespace PatternFileCategorizer.FileMatcher
{
    public class DocxMatcher : IFileMatcher
    {
        public IEnumerable<string> GetMatches(string filePath, List<Regex> regexes)
        {
            try
            {
                using var doc = WordprocessingDocument.Open(filePath, false);
                string text = doc.MainDocumentPart?.Document?.Body?.InnerText ?? "";
                return MatcherUtils.ExtractMatches(text, regexes);
            }
            catch 
            { 
                return Enumerable.Empty<string>(); 
            }
        }
    }
}
