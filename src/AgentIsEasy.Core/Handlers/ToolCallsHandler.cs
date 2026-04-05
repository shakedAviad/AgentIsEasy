
using AgentIsEasy.Core.Models;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AgentIsEasy.Core.Handlers;

public class ToolCallsHandler(Action<ToolCallingDetails> toolCallDetails)
{
    public async ValueTask<object?> ToolCallingMiddlewareAsync(AIAgent agent, FunctionInvocationContext context, Func<FunctionInvocationContext, CancellationToken, ValueTask<object?>> next, CancellationToken cancellationToken)
    {
        var result = await next(context, cancellationToken);

        toolCallDetails.Invoke(new() { Context = context });

        return result;
    }
}
