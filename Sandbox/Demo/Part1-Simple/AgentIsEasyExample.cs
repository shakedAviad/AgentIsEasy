using AgentIsEasy.OpenAI.Factories;

namespace Demo.Part1;

public static class AgentIsEasyExample
{
    public static async Task RunAsync(string apiKey)
    {
        var factory = new OpenAIAgentFactory(apiKey);

        var agent = factory.CreateAgent(
            model: "gpt-4o",
            instructions: "You are a helpful assistant.",
            name: "Assistant");

        var response = await agent.RunAsync("What is 2 + 2?");

        Console.WriteLine($"Agent Is Easy: {response}");

    }
}