using OData.Schema.Validation.Utils;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
    })
    .Build();

static TService Get<TService>(IHost host)
    where TService : notnull =>
    host.Services.GetRequiredService<TService>();

static async Task StartAnalysisAsync(ActionInputs inputs, IHost host)
{
    var updatedMetrics = "100%";
    var title = "OData Inspector";
    var summary = "The summary";

    string sourceBranch = "patch-1";
    string destinationBranch = "patch-1";
    string userName = "wachugamaina";
    var sourceSchemas = await GitUtilities.GetSchemasFromBranch(userName, sourceBranch);
    var destinationSchemas = await GitUtilities.GetSchemasFromBranch(userName, sourceBranch);

    ParentValidator validator = new ParentValidator(sourceSchemas, destinationSchemas);
    validator.RunValidateion();

    await Task.Delay(1);

    // https://docs.github.com/actions/reference/workflow-commands-for-github-actions#setting-an-output-parameter
    Console.WriteLine($"::set-output name=updated-metrics::{updatedMetrics}");
    Console.WriteLine($"::set-output name=summary-title::{title}");
    Console.WriteLine($"::set-output name=summary-details::{summary}");

    Environment.Exit(0);
}

var parser = Default.ParseArguments<ActionInputs>(() => new(), args);
parser.WithNotParsed(
    errors =>
    {
        Get<ILoggerFactory>(host)
            .CreateLogger("OData.Inspector")
            .LogError(
                string.Join(Environment.NewLine, errors.Select(error => error.ToString())));
        
        Environment.Exit(2);
    });

await parser.WithParsedAsync(options => StartAnalysisAsync(options, host));
await host.RunAsync();
