using Demo.Part2.ToolsAndMcp;
using Demo.Part2_ToolsAndMcp;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Client;
using OpenAI;
using OpenAI.Chat;
using System.Text;

namespace Demo.Part3.Middleware;

public static class MicrosoftExample
{
    public static async Task RunAsync(string openAiApiKey, string gitHubPat)
    {
        Console.WriteLine("Microsoft Agent Framework:");

        var localTools = new ToolsForMicrosoft();

        var githubUsernameTool = AIFunctionFactory.Create(
            typeof(ToolsForMicrosoft).GetMethod(nameof(ToolsForMicrosoft.GetGitHubUsername))!,
            localTools);

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

        var allTools = new List<AITool> { githubUsernameTool };
        allTools.AddRange(gitHubTools.Cast<AITool>());

        var client = new OpenAIClient(openAiApiKey);

        var baseAgent = client
            .GetChatClient("gpt-4o")
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

        var agent = baseAgent
            .AsBuilder()
            .Use(async (agent, context, next, cancellationToken) =>
            {
                var toolDetails = new StringBuilder($"Tool call: {context.Function.Name}");

                if (context.Arguments.Any())
                {
                    toolDetails.AppendLine($" (Args: {string.Join(",", context.Arguments.Select(x => $"[{x.Key} = {x.Value}]"))} )");
                }

                Console.WriteLine(toolDetails.ToString());

                return await next(context, cancellationToken);
            })
            .UseLogging(LoggerFactory.Create(builder => builder.AddConsole()))
            .Build();

        var prompt = "Get my public repositories GitHub.";
        var response = await agent.RunAsync<List<GitHubResponseModel>>(prompt);

        response.Result.ForEach(repository =>
        {
            Console.WriteLine();
            Console.WriteLine(repository);
            Console.WriteLine();
        });
    }
}