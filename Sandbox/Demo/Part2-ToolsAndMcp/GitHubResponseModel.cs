namespace Demo.Part2_ToolsAndMcp;

public class GitHubResponseModel
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Language { get; set; }

    public override string ToString()
    {
        return $"""            
            Name: {Name}
            Url: {Url}
            Language: {Language}            
            """;
    }
}
