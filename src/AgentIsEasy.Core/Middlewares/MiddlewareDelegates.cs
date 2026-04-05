using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AgentIsEasy.Core.Middlewares;

public class MiddlewareDelegates
{
    public delegate ValueTask<object?> ToolCallingMiddleware(
        AIAgent agent,
        FunctionInvocationContext context,
        Func<FunctionInvocationContext, CancellationToken, ValueTask<object?>> next,
        CancellationToken cancellationToken);
}
