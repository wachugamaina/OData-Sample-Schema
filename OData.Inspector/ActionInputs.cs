namespace OData.Inspector;

public class ActionInputs
{
    public ActionInputs()
    {
        if (Environment.GetEnvironmentVariable(
            "GREETINGS") is { Length: > 0 } greetings)
        {
            Console.WriteLine(greetings);
        }
    }

    [Option('b', "branch",
        Required = true,
        HelpText = "The owner, for example: \"dotnet\". Assign from `github.repository_owner`.")]
    public string SourceBranch { get; set; } = null!;


    [Option('t', "targetBranch",
        Required = true,
        HelpText = "The owner, for example: \"dotnet\". Assign from `github.repository_owner`.")]
    public string TargetBranch { get; set; } = null!;
}
