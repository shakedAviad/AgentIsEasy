using Microsoft.Extensions.AI;
using System.Reflection;

namespace AgentIsEasy.Tools;

public class ToolsFactory
{
    public IList<AITool> GetTools(params Type[] types)
    {
        return [.. types.SelectMany(GetTools)];
    }

    public IList<AITool> GetTools(params object[] objectInstances)
    {
        return [.. objectInstances.SelectMany(GetTools)];
    }
    private IList<AITool> GetTools(Type type)
    {
        if (type.IsAbstract)
        {
            return [.. GetMethodsWithAttribute(type).Select(methodInfo =>
            {
                var definition = methodInfo.GetCustomAttribute<AIToolAttribute>()!;
                return AIFunctionFactory.Create(methodInfo, name: definition.Name, description: definition.Description, target: null);
            }).Cast<AITool>()];
        }

        return GetTools(Activator.CreateInstance(type)!);
    }

    private IList<AITool> GetTools(object objectInstance)
    {
        return [.. GetMethodsWithAttribute(objectInstance.GetType()).Select(methodInfo =>
        {
            var definition = methodInfo.GetCustomAttribute<AIToolAttribute>()!;
            return AIFunctionFactory.Create(methodInfo, objectInstance, name: definition.Name, description: definition.Description);
        }).Cast<AITool>()];
    }

    private List<MethodInfo> GetMethodsWithAttribute(Type type)
    {
        var methods = type.GetMethods(
            BindingFlags.Public
            | BindingFlags.NonPublic
            | BindingFlags.Static
            | BindingFlags.Instance
            | BindingFlags.FlattenHierarchy);

        return [.. methods.Where(x => x.GetCustomAttribute<AIToolAttribute>() != null)];
    }
}
