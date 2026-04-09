using AgentIsEasy.ModelContextProtocol.Models;
using AgentIsEasy.Tools;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace AgentIsEasy.ModelContextProtocol;

public static class Extensions
{
    extension(ToolsFactory factory)
    {
        public async Task<McpClientTools> GetToolsFromRemoteMcpAsync(string remoteMcpUrl, IDictionary<string, string>? additionalHeaders = null)
        {
            var options = new HttpClientTransportOptions()
            {
                TransportMode = HttpTransportMode.AutoDetect,
                Endpoint = new Uri(remoteMcpUrl),
                AdditionalHeaders = additionalHeaders,
            };

            return await factory.GetToolsFromRemoteMcpAsync(options);
        }

        public async Task<McpClientTools> GetToolsFromRemoteMcpAsync(HttpClientTransportOptions options)
        {
            var client = await McpClient.CreateAsync(new HttpClientTransport(options));
            var mcpTools = await client.ListToolsAsync();

            return new McpClientTools
            {
                McpClient = client,
                Tools = mcpTools.Cast<AITool>().ToList()
            };
        }

        public async Task<McpClientTools> GetToolsFromLocalMcpAsync(string command, IList<string>? arguments)
        {
            var options = new StdioClientTransportOptions()
            {
                Command = command,
                Arguments = arguments,
            };

            return await GetToolsFromLocalMcpAsync(factory, options);
        }

        public async Task<McpClientTools> GetToolsFromLocalMcpAsync(StdioClientTransportOptions options)
        {
            var client = await McpClient.CreateAsync(new StdioClientTransport(options));
            var mcpTools = await client.ListToolsAsync();

            return new McpClientTools
            {
                McpClient = client,
                Tools = mcpTools.Cast<AITool>().ToList()
            };
        }
    }

}
