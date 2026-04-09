using AgentIsEasy.Core;
using AgentIsEasy.Core.Agents;
using AgentIsEasy.Core.Interfaces;
using AgentIsEasy.Core.Models;
using AgentIsEasy.OpenAI.Enums;
using AgentIsEasy.OpenAI.Models;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Responses;
#pragma warning disable OPENAI001

namespace AgentIsEasy.OpenAI.Factories;

public class OpenAIAgentFactory : IAgentProviderFactory
{
    public OpenAIClient Client { get; }

    public OpenAIAgentFactory(string apiKey)
    {
        Client = new OpenAIClient(apiKey);
    }

    public OpenAIAgentFactory(OpenAIClient client)
    {
        Client = client;
    }

    public Agent CreateAgent(string model, string? instructions = null, string? name = null, IList<AITool>? tools = null)
    {
        var chatClientOpenAIOptions = new ChatClientAgentOptions()
        {
            Name = name,
            ChatOptions = new ChatOptions
            {
                Tools = tools,
                Instructions = instructions
            }
        };

        var innerAgent = Client.GetChatClient(model).AsAIAgent(chatClientOpenAIOptions);

        return new OpenAIAgent(innerAgent);
    }

    public Agent CreateAgent(AgentOptions options)
    {
        var chatClientAgent = options is OpenAIOptions openAIOptions ? GetChatClientAgent(openAIOptions) : GetChatClientAgent(options);
        var innerAgent = chatClientAgent.ApplyMiddleware(options);

        return new OpenAIAgent(innerAgent);
    }

    private ChatClientAgent GetChatClientAgent(OpenAIOptions options)
    {
        var chatClientOpenAIOptions = CreateChatClientOpenAIOptions(options);

        return options.ClientType switch
        {
            ClientType.ChatClient => Client.GetChatClient(options.Model)
                .AsAIAgent(
                    chatClientOpenAIOptions,
                    options.ClientFactory,
                    options.LoggerFactory,
                    options.Services),
            ClientType.ResponsesApi => Client.GetResponsesClient()
                .AsAIAgent(
                    chatClientOpenAIOptions,
                    options.Model,
                    options.ClientFactory,
                    options.LoggerFactory,
                    options.Services),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private ChatClientAgentOptions CreateChatClientOpenAIOptions(OpenAIOptions options)
    {
        var chatOptions = new ChatOptions
        {
            Tools = options.Tools,
            MaxOutputTokens = options.MaxOutputTokens.HasValue ? options.MaxOutputTokens.Value : null,
            Temperature = options.Temperature.HasValue ? options.Temperature.Value : null,
            Instructions = options.Instructions
        };

        var reasoningEffortAsString = options.ReasoningEffort switch
        {
            OpenAIReasoningEffort.None => "none",
            OpenAIReasoningEffort.Minimal => "minimal",
            OpenAIReasoningEffort.Low => "low",
            OpenAIReasoningEffort.Medium => "medium",
            OpenAIReasoningEffort.High => "high",
            OpenAIReasoningEffort.ExtraHigh => "xhigh",
            _ => null
        };

        chatOptions.RawRepresentationFactory = _ => options.ClientType switch
        {
            ClientType.ChatClient => InitRawOptionsForChatClient(options, reasoningEffortAsString),
            ClientType.ResponsesApi => InitRawOptionsForResponsesApi(options, reasoningEffortAsString),
            _ => null
        };

        var chatClientOpenAIOptions = new ChatClientAgentOptions()
        {
            Id = options.Id,
            Name = options.Name,
            Description = options.Description,
            AIContextProviders = options.AIContextProviders,
            ChatHistoryProvider = options.ChatHistoryProvider,
        };

        chatClientOpenAIOptions.ChatOptions = chatOptions;

        return chatClientOpenAIOptions;

        object? InitRawOptionsForChatClient(OpenAIOptions options, string? reasoningEffortAsString)
        {
            ChatCompletionOptions? rawOptions = null;

            if (!string.IsNullOrWhiteSpace(reasoningEffortAsString) && !OpenAIChatModels.NonReasoningModels.Contains(options.Model))
            {
                rawOptions = new ChatCompletionOptions();
                rawOptions.ReasoningEffortLevel = new ChatReasoningEffortLevel(reasoningEffortAsString);
            }

            if (options.ServiceTier.HasValue)
            {
                rawOptions = rawOptions is null ? new ChatCompletionOptions() : rawOptions;
                rawOptions.ServiceTier = options.ServiceTier switch
                {
                    OpenAIServiceTier.Auto => new ChatServiceTier("auto"),
                    OpenAIServiceTier.Flex => new ChatServiceTier("flex"),
                    OpenAIServiceTier.Default => new ChatServiceTier("default"),
                    OpenAIServiceTier.Priority => new ChatServiceTier("priority"),
                    null => (ChatServiceTier?)null,
                    _ => throw new ArgumentOutOfRangeException(nameof(options.ServiceTier), options.ServiceTier, null)
                };
            }

            return rawOptions;
        }

        object? InitRawOptionsForResponsesApi(OpenAIOptions options, string? reasoningEffortAsString)
        {
            CreateResponseOptions? rawOptions = null;
            if (!string.IsNullOrWhiteSpace(reasoningEffortAsString) && !OpenAIChatModels.NonReasoningModels.Contains(options.Model))
            {
                rawOptions = new CreateResponseOptions();
                rawOptions.ReasoningOptions = new ResponseReasoningOptions
                {
                    ReasoningEffortLevel = new ResponseReasoningEffortLevel(reasoningEffortAsString),
                    ReasoningSummaryVerbosity = options.ReasoningSummaryVerbosity switch
                    {
                        OpenAIReasoningSummaryVerbosity.Auto => ResponseReasoningSummaryVerbosity.Auto,
                        OpenAIReasoningSummaryVerbosity.Concise => ResponseReasoningSummaryVerbosity.Concise,
                        OpenAIReasoningSummaryVerbosity.Detailed => ResponseReasoningSummaryVerbosity.Detailed,
                        null => (ResponseReasoningSummaryVerbosity?)null,
                        _ => throw new ArgumentOutOfRangeException()
                    }
                };
            }

            if (options.ServiceTier.HasValue)
            {
                rawOptions = rawOptions is null ? new CreateResponseOptions() : rawOptions;
                rawOptions.ServiceTier = options.ServiceTier switch
                {
                    OpenAIServiceTier.Auto => new ResponseServiceTier("auto"),
                    OpenAIServiceTier.Flex => new ResponseServiceTier("flex"),
                    OpenAIServiceTier.Default => new ResponseServiceTier("default"),
                    OpenAIServiceTier.Priority => new ResponseServiceTier("priority"),
                    null => (ResponseServiceTier?)null,
                    _ => throw new ArgumentOutOfRangeException(nameof(options.ServiceTier), options.ServiceTier, null)
                };
            }

            return rawOptions;
        }
    }
    private ChatClientAgentOptions CreateChatClientOpenAIOptions(AgentOptions options)
    {
        var chatOptions = new ChatOptions
        {
            Tools = options.Tools,
            Instructions = options.Instructions
        };

        var chatClientOpenAIOptions = new ChatClientAgentOptions()
        {
            Id = options.Id,
            Name = options.Name,
            Description = options.Description,
        };

        chatClientOpenAIOptions.ChatOptions = chatOptions;

        return chatClientOpenAIOptions;

    }
    private ChatClientAgent GetChatClientAgent(AgentOptions options)
    {
        var chatClientOpenAIOptions = CreateChatClientOpenAIOptions(options);

        return Client.GetChatClient(options.Model)
                .AsAIAgent(
                    chatClientOpenAIOptions,
                    options.ClientFactory,
                    options.LoggerFactory,
                    options.Services);
    }
}