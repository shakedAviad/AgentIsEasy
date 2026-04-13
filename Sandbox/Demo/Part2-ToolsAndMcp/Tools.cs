using AgentIsEasy.Tools;
using System.ComponentModel;

namespace Demo.Part2.ToolsAndMcp;

public class ToolsForAgentIsEasy
{
    [AITool("get_github_username", "Returns the GitHub username.")]
    public string GetGitHubUsername()
    {
        return "shakedaviad";
    }
}

public class ToolsForMicrosoft
{
    [DisplayName("get_github_username")]
    [Description("Returns the GitHub username.")]
    public string GetGitHubUsername()
    {
        return "shakedaviad";
    }
}