namespace AgentIsEasy.OpenAI.Models;

public class OpenAIChatModels
{
    public const string Gpt54Mini = "gpt-5.4-mini";

    public const string Gpt54Nano = "gpt-5.4-nano";

    public const string Gpt54Pro = "gpt-5.4-pro";

    public const string Gpt54 = "gpt-5.4";

    public const string Gpt52Pro = "gpt-5.2-pro";

    public const string Gpt52 = "gpt-5.2";

    public const string Gpt51 = "gpt-5.1";

    public const string Gpt53Codex = "gpt-5.3-codex";

    public const string Gpt52Codex = "gpt-5.2-codex";

    public const string Gpt51CodexMax = "gpt-5.1-codex";

    public const string Gpt51Codex = "gpt-5.1-codex";

    public const string Gpt5Codex = "gpt-5-codex";

    public const string Gpt5Pro = "gpt-5-pro";

    public const string Gpt5 = "gpt-5";

    public const string Gpt5Mini = "gpt-5-mini";

    public const string Gpt5Nano = "gpt-5-nano";

    public const string Gpt41 = "gpt-4.1";

    public const string Gpt41Mini = "gpt-4.1-mini";

    public const string Gpt41Nano = "gpt-4.1-nano";

    public const string Gpt4O = "gpt-4o";

    public const string Gpt4OMini = "gpt-4o-mini";

    public static string[] NonReasoningModels = [Gpt41, Gpt41Mini, Gpt41Nano, Gpt4O, Gpt4OMini];

    public static string[] ReasoningModels = [Gpt5, Gpt5Pro, Gpt51, Gpt5Mini, Gpt5Nano, Gpt5Codex, Gpt51Codex, Gpt51CodexMax, Gpt52Pro, Gpt52, Gpt52Codex, Gpt53Codex, Gpt54, Gpt54Pro];
}