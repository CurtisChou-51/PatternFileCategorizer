using Microsoft.Extensions.Configuration;
using PatternFileCategorizer.FileMatcher;
using System.Text.RegularExpressions;

namespace PatternFileCategorizer
{
    internal class Program
    {
        private static readonly Dictionary<string, IFileMatcher> _matchers = new(StringComparer.OrdinalIgnoreCase)
        {
            { ".docx", new DocxMatcher() },
            { ".pptx", new PptxMatcher() }
        };
        private static readonly IFileMatcher _defaultMatcher = new DefaultMatcher();

        static void Main(string[] args)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var config = configuration.Get<AppConfig>() ?? new AppConfig();
            if (string.IsNullOrEmpty(config.SourceDirectory) || !Directory.Exists(config.SourceDirectory))
            {
                Console.WriteLine("來源路徑無效。");
                return;
            }

            List<Regex> regexes = config.Patterns.Select(p => new Regex(p, RegexOptions.Compiled)).ToList();
            IEnumerable<string> files = Directory.EnumerateFiles(config.SourceDirectory, "*.*", SearchOption.AllDirectories);

            Console.WriteLine($"[開始處理] 來源: {config.SourceDirectory}");
            foreach (string file in files)
            {
                string ext = Path.GetExtension(file);
                IFileMatcher matcher = _matchers.TryGetValue(ext, out var m) ? m : _defaultMatcher;

                Console.Write($"處理中: {Path.GetFileName(file)} ... ");

                var matches = matcher.GetMatches(file, regexes).ToList();
                if (matches.Count != 0)
                {
                    Console.WriteLine($"找到匹配: {string.Join(", ", matches)}");
                    foreach (var category in matches)
                    {
                        string destDir = Path.Combine(config.TargetDirectory, category);
                        Directory.CreateDirectory(destDir);
                        File.Copy(file, Path.Combine(destDir, Path.GetFileName(file)), true);
                    }
                }
                else
                {
                    File.Copy(file, Path.Combine("未分類", Path.GetFileName(file)), true);
                }
            }
        }
    }
}
