using Microsoft.Extensions.AI;
using OpenAI;

namespace AgentIsEasy.OpenAI.Factories;

public class OpenAIEmbeddingFactory
{
    public OpenAIClient Client { get; }

    public OpenAIEmbeddingFactory(string apiKey)
    {
        Client = new OpenAIClient(apiKey);
    }

    public OpenAIEmbeddingFactory(OpenAIClient client)
    {
        Client = client;
    }

    public IEmbeddingGenerator<string, Embedding<float>> GetEmbeddingGenerator(string model)
    {
        return Client.GetEmbeddingClient(model).AsIEmbeddingGenerator();
    }

    public async Task<Embedding<float>> GenerateAsync(string value, string model, CancellationToken cancellationToken = default)
    {
        return await GenerateAsync(value, model, null, cancellationToken);
    }

    public async Task<GeneratedEmbeddings<Embedding<float>>> GenerateAsync(IEnumerable<string> values, string model, CancellationToken cancellationToken = default)
    {
        return await GenerateAsync(values, model, null, cancellationToken);
    }

    public async Task<Embedding<float>> GenerateAsync(string value, string model, EmbeddingGenerationOptions? options, CancellationToken cancellationToken = default)
    {
        var generator = GetEmbeddingGenerator(model);
        return await generator.GenerateAsync(value, options, cancellationToken);
    }


    public async Task<GeneratedEmbeddings<Embedding<float>>> GenerateAsync(IEnumerable<string> values, string model, EmbeddingGenerationOptions? options, CancellationToken cancellationToken = default)
    {
        var generator = GetEmbeddingGenerator(model);
        return await generator.GenerateAsync(values, options, cancellationToken);
    }
}
