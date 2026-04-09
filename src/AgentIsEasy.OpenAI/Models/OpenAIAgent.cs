using AgentIsEasy.Core.Agents;
using Microsoft.Agents.AI;

namespace AgentIsEasy.OpenAI.Models;

public class OpenAIAgent(AIAgent innerAgent) : Agent(innerAgent);