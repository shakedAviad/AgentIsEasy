using AgentIsEasy.Core.Handlers;
using AgentIsEasy.Core.Models;
using Microsoft.Agents.AI;

namespace AgentIsEasy.Core;

public static class Extensions
{
    extension(AIAgent agent)
    {
        public AIAgent ApplyMiddleware(AgentOptions options)
        {
            var builder = agent.AsBuilder();

            if (options.RawToolCallDetails is not null)
            {
                builder = builder.Use(new ToolCallsHandler(options.RawToolCallDetails).ToolCallingMiddlewareAsync);
            }

            if (options.ToolCallingMiddleware is not null)
            {
                builder = builder.Use(options.ToolCallingMiddleware.Invoke);
            }

            if (options.LoggingMiddleware is not null)
            {
                builder = builder.UseLogging(options.LoggingMiddleware.LoggerFactory, options.LoggingMiddleware.Configure);
            }

            agent = builder.Build(options.Services);

            return agent;
        }
    }
}
