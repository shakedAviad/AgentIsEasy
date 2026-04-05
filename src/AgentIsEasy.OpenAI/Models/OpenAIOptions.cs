using AgentIsEasy.Core.Models;
using AgentIsEasy.OpenAI.Enums;
using Microsoft.Agents.AI;

namespace AgentIsEasy.OpenAI.Models;

public class OpenAIOptions : AgentOptions
{
    public ClientType ClientType { get; set; } = Enums.ClientType.ChatClient;
    public int? MaxOutputTokens { get; set; }
    public float? Temperature { get; set; }
    public OpenAIReasoningEffort? ReasoningEffort { get; set; }
    public OpenAIReasoningSummaryVerbosity? ReasoningSummaryVerbosity { get; set; }
    public ChatHistoryProvider? ChatHistoryProvider { get; set; }
    public IEnumerable<AIContextProvider>? AIContextProviders { get; set; }
    public OpenAIServiceTier? ServiceTier { get; set; }
}
