using DocumentFormat.OpenXml.Packaging;
using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Drawing;

namespace PatternFileCategorizer.FileMatcher
{
    public class PptxMatcher : IFileMatcher
    {
        public IEnumerable<string> GetMatches(string filePath, List<Regex> regexes)
        {
            try
            {
                using var ppt = PresentationDocument.Open(filePath, false);
                var slides = ppt.PresentationPart?.SlideParts;
                if (slides == null)
                    return Enumerable.Empty<string>();

                var allText = new StringBuilder();
                foreach (var slide in slides)
                {
                    if (slide.Slide == null)
                        continue;
                    foreach (var t in slide.Slide.Descendants<Text>())
                    {
                        allText.Append(t.Text).Append(' ');
                    }
                }
                return MatcherUtils.ExtractMatches(allText.ToString(), regexes);
            }
            catch 
            { 
                return Enumerable.Empty<string>(); 
            }
        }
    }
}
