using AgentIsEasy.Core.Agents;
using AgentIsEasy.Core.Models;
using Microsoft.Extensions.AI;

namespace AgentIsEasy.Core.Interfaces;

public interface IAgentProviderFactory
{
    Agent CreateAgent(AgentOptions options);
    Agent CreateAgent(string model, string? instructions = null, string? name = null, IList<AITool>? tools = null);
}
