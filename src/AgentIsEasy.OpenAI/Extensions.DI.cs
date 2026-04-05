using AgentIsEasy.OpenAI.Factories;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;

namespace AgentIsEasy.OpenAI;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOpenAIAgentFactory(OpenAIClient client)
        {
            return services.AddSingleton(new OpenAIAgentFactory(client));
        }
        public IServiceCollection AddOpenAIAgentFactory(string apiKey)
        {
            return services.AddSingleton(new OpenAIAgentFactory(apiKey));
        }
        public IServiceCollection AddOpenAIEmbeddingFactory(OpenAIClient client)
        {
            return services.AddSingleton(new OpenAIEmbeddingFactory(client));
        }
        public IServiceCollection AddOpenAIEmbeddingFactory(string apiKey)
        {
            return services.AddSingleton(new OpenAIEmbeddingFactory(apiKey));
        }

    }
}
