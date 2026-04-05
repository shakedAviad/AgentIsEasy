using Microsoft.Agents.AI;

namespace AgentIsEasy.Core.Middlewares;

public class OpenTelemetryMiddleware(string? source, Action<OpenTelemetryAgent> configure)
{
    public string? Source { get; } = source;
    public Action<OpenTelemetryAgent> Configure { get; } = configure;
}
