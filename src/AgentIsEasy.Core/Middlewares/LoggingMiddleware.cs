using Microsoft.Agents.AI;
using Microsoft.Extensions.Logging;

namespace AgentIsEasy.Core.Middlewares;

public class LoggingMiddleware(ILoggerFactory? loggerFactory = null, Action<LoggingAgent>? configure = null)
{
    public ILoggerFactory? LoggerFactory { get; } = loggerFactory;
    public Action<LoggingAgent>? Configure { get; } = configure;
}




