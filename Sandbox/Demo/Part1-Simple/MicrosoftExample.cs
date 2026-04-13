using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;

namespace Demo.Part1;

public static class MicrosoftExample
{
    public static async Task RunAsync(string apiKey)
    {
        var client = new OpenAIClient(apiKey);
        var chatClient = client.GetChatClient("gpt-4o");
        var chatOptions = new ChatOptions
        {
            Instructions = "You are a helpful assistant."
        };
        var agentOptions = new ChatClientAgentOptions
        {
            Name = "Assistant",
            ChatOptions = chatOptions
        };
        var agent = chatClient.AsAIAgent(agentOptions);

        var response = await agent.RunAsync("What is 2 + 2?");

        Console.WriteLine($"Microsoft Agent Framework: {response}");
    }
}
