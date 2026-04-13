using AgentIsEasy.ModelContextProtocol;
using AgentIsEasy.OpenAI.Factories;
using AgentIsEasy.Tools;
using Demo.Part2_ToolsAndMcp;
using Microsoft.Extensions.AI;

namespace Demo.Part2.ToolsAndMcp;

public static class AgentIsEasyExample
{
    public static async Task RunAsync(string openAiApiKey, string gitHubPat)
    {
        var toolsFactory = new ToolsFactory();
        var localTools = toolsFactory.GetTools(new ToolsForAgentIsEasy());

        await using var gitHubMcpTools = await toolsFactory.GetToolsFromRemoteMcpAsync(
            "https://api.githubcopilot.com/mcp/",
            new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {gitHubPat}"
            });

        var allTools = new List<AITool>();

        allTools.AddRange(localTools);
        allTools.AddRange(gitHubMcpTools.Tools);

        var factory = new OpenAIAgentFactory(openAiApiKey);

        var agent = factory.CreateAgent(
            model: "gpt-5-mini",
            instructions: """
                You are a helpful assistant.                       
                Use tools when needed.
                If you need the GitHub username, call the relevant tool.
                
                Return the repositories in this exact format:
                Name: <repository name>
                Url: <repository url>
                Language: <main language>
                """,
            name: "GitHubAssistant",
            tools: allTools);

        var prompt = "Get my public repositories GitHub.";
        var response = await agent.RunAsync<List<GitHubResponseModel>>(prompt);

        Console.WriteLine("Agent Is Easy:");
        response.Result.ForEach(repository =>
        {
            Console.WriteLine();
            Console.WriteLine(repository);
            Console.WriteLine();
        });
    }
}