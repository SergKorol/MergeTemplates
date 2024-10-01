namespace MergeTemplates;

internal static class Program
{
    private static void Main(string[] args)
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

        var readmeTemplate = File.ReadAllText(readmeTemplatePath);
        var articlesTemplate = File.ReadAllText(articlesTemplatePath);
        var weathersTemplate = File.ReadAllText(weathersTemplatePath);

        var mergedContent = readmeTemplate
            .Replace("{{ template \"articles.md.tpl\" }}", articlesTemplate)
            .Replace("{{ template \"weathers.md.tpl\" }}", weathersTemplate);

        File.WriteAllText(outputFilePath, mergedContent);

        Console.WriteLine("Templates merged successfully into " + outputFilePath);
    }
}