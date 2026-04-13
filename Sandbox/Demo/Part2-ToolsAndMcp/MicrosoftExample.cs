using Demo.Part2_ToolsAndMcp;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using OpenAI;
using OpenAI.Chat;

namespace Demo.Part2.ToolsAndMcp;

public static class MicrosoftExample
{
    public static async Task RunAsync(string openAiApiKey, string gitHubPat)
    {
        var tools = new ToolsForMicrosoft();
        var githubUsernameTool = AIFunctionFactory.Create(
            typeof(ToolsForMicrosoft).GetMethod(nameof(ToolsForMicrosoft.GetGitHubUsername))!,
            tools);

        await using var gitHubMcpClient = await McpClient.CreateAsync(
            new HttpClientTransport(
                new HttpClientTransportOptions
                {
                    TransportMode = HttpTransportMode.StreamableHttp,
                    Endpoint = new Uri("https://api.githubcopilot.com/mcp/"),
                    AdditionalHeaders = new Dictionary<string, string>
                    {
                        ["Authorization"] = $"Bearer {gitHubPat}"
                    }
                }));

        var gitHubTools = await gitHubMcpClient.ListToolsAsync();
        var allTools = new List<AITool>
        {
            githubUsernameTool
        };

        allTools.AddRange(gitHubTools.Cast<AITool>());

        var client = new OpenAIClient(openAiApiKey);

        var agent = client
            .GetChatClient("gpt-5-mini")
            .AsAIAgent(new ChatClientAgentOptions
            {
                Name = "GitHubAssistant",
                ChatOptions = new ChatOptions
                {
                    Instructions = """
                        You are a helpful assistant.                       
                        Use tools when needed.
                        If you need the GitHub username, call the relevant tool.

                        Return the repositories in this exact format:
                        Name: <repository name>
                        Url: <repository url>
                        Language: <main language>
                        """,
                    Tools = allTools
                }
            });

        var prompt = "Get my public repositories GitHub.";
        var response = await agent.RunAsync<List<GitHubResponseModel>>(prompt);

        Console.WriteLine("Microsoft Agent Framework:");
        response.Result.ForEach(repository =>
        {
            Console.WriteLine();
            Console.WriteLine(repository);
            Console.WriteLine();
        });
    }
}