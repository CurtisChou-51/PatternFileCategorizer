using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace PatternFileCategorizer
{
    internal class Program
    {
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
                // todo
            }
        }
    }
}
