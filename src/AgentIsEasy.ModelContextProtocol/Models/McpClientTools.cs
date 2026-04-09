using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace AgentIsEasy.ModelContextProtocol.Models;

public class McpClientTools : IAsyncDisposable
{
    public required McpClient McpClient { get; set; }

    public required IList<AITool> Tools { get; set; }

    public async ValueTask DisposeAsync()
    {
        await McpClient.DisposeAsync();
    }
}
