
/*  
    Initialize the demo by setting up the necessary environment variables for API keys. 
    Make sure to set the OPENAI_API_KEY and GITHUB_PAT environment variables before running the demo.
    

    Environment.SetEnvironmentVariable("OPENAI_API_KEY", <Your OPENAI_API_KEY>);
    Environment.SetEnvironmentVariable("GITHUB_PAT", <Your GITHUB_PAT>);
 */


var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
var gitHubPat = Environment.GetEnvironmentVariable("GITHUB_PAT");

if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(gitHubPat))
{
    throw new InvalidOperationException("Please set the OPENAI_API_KEY and GITHUB_PAT environment variables.");
}

#region Part 1: Simple Agent
Console.WriteLine("=== PART 1: SIMPLE AGENT ===");
Console.WriteLine("Question: What is 2 + 2?");

await Demo.Part1.MicrosoftExample.RunAsync(apiKey);

Console.WriteLine(string.Empty.PadLeft(Console.WindowWidth, '-'));

await Demo.Part1.AgentIsEasyExample.RunAsync(apiKey);
#endregion

#region Part 2: Tools and MCP
Console.Clear();
Console.WriteLine("=== PART 2: TOOLS AND MCP ===");

await Demo.Part2.ToolsAndMcp.MicrosoftExample.RunAsync(apiKey, gitHubPat);

Console.WriteLine(string.Empty.PadLeft(Console.WindowWidth, '-'));

await Demo.Part2.ToolsAndMcp.AgentIsEasyExample.RunAsync(apiKey, gitHubPat);
#endregion

#region Part 3: Middleware Agent
Console.Clear();
Console.WriteLine("=== PART 3: MIDDLEWARE ===");
Console.WriteLine();

await Demo.Part3.Middleware.MicrosoftExample.RunAsync(apiKey, gitHubPat);

Console.WriteLine(string.Empty.PadLeft(Console.WindowWidth, '-'));

await Demo.Part3.Middleware.AgentIsEasyExample.RunAsync(apiKey, gitHubPat);
#endregion

