using AgentIsEasy.Core.Middlewares;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AgentIsEasy.Core.Models;

public class AgentOptions
{
    public required string Model { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Instructions { get; set; }

    public IList<AITool>? Tools { get; set; }

    public IServiceProvider? Services { get; set; }

    public ILoggerFactory? LoggerFactory { get; set; }

    public MiddlewareDelegates.ToolCallingMiddleware? ToolCallingMiddleware { get; set; }

    public OpenTelemetryMiddleware? OpenTelemetryMiddleware { get; set; }

    public LoggingMiddleware? LoggingMiddleware { get; set; }

    public Action<ToolCallingDetails>? RawToolCallDetails { get; set; }

    public Action<ChatClientAgentOptions>? AdditionalChatClientAgentOptions { get; set; }

    public Func<IChatClient, IChatClient>? ClientFactory { get; set; }

}
