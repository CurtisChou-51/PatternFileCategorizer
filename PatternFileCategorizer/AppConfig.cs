namespace PatternFileCategorizer
{
    public class AppConfig
    {
        public List<string> Patterns { get; set; } = new();
        public string SourceDirectory { get; set; } = "";
        public string TargetDirectory { get; set; } = "";
    }
}
