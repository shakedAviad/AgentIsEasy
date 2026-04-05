using Microsoft.Agents.AI;

namespace AgentIsEasy.Core.Agents;

public class OpenAIAgent(AIAgent innerAgent) : Agent(innerAgent);