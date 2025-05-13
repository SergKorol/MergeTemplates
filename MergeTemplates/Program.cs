namespace MergeTemplates;

internal static class Program
{
    private static void Main(string[] args)
    {
        try
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: README.md 'templates/README.md.tpl' '.tmp/articles.md.tpl' '.tmp/weathers.md.tpl'");
                return;
            }

            var outputFilePath = args[0];
            var readmeTemplatePath = args[1];
            var articlesTemplatePath = args[2];
            var weathersTemplatePath = args[3];

            Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
            Console.WriteLine($"Output File: {outputFilePath}");
            Console.WriteLine($"README Template: {readmeTemplatePath}");
            Console.WriteLine($"Articles Template: {articlesTemplatePath}");
            Console.WriteLine($"Weathers Template: {weathersTemplatePath}");

            EnsureDirectoryExists(outputFilePath);
            EnsureDirectoryExists(articlesTemplatePath);
            EnsureDirectoryExists(weathersTemplatePath);

            if (!File.Exists(readmeTemplatePath))
            {
                throw new FileNotFoundException($"README template file not found: {readmeTemplatePath}");
            }

            if (!File.Exists(articlesTemplatePath))
            {
                throw new FileNotFoundException($"Articles template file not found: {articlesTemplatePath}");
            }

            if (!File.Exists(weathersTemplatePath))
            {
                throw new FileNotFoundException($"Weathers template file not found: {weathersTemplatePath}");
            }

            var readmeTemplate = File.ReadAllText(readmeTemplatePath);
            var articlesTemplate = File.ReadAllText(articlesTemplatePath);
            var weathersTemplate = File.ReadAllText(weathersTemplatePath);

            var mergedContent = readmeTemplate
                .Replace("{{ template \"articles.md.tpl\" }}", articlesTemplate)
                .Replace("{{ template \"weathers.md.tpl\" }}", weathersTemplate);

            File.WriteAllText(outputFilePath, mergedContent);
            Console.WriteLine("Templates merged successfully into " + outputFilePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
            Environment.Exit(1);
        }
    }

    private static void EnsureDirectoryExists(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (string.IsNullOrEmpty(directory) || Directory.Exists(directory)) return;
        Console.WriteLine($"Creating directory: {directory}");
        Directory.CreateDirectory(directory);
    }
}