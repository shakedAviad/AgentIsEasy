namespace AgentIsEasy.Tools;

public class AIToolAttribute(string? name = null, string? description = null) : Attribute
{
    public string? Name { get; } = name;

    public string? Description { get; } = description;
}
