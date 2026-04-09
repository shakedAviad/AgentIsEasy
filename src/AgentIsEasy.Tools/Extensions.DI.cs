using Microsoft.Extensions.DependencyInjection;

namespace AgentIsEasy.Tools;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAIToolFactory()
        {
            return services.AddSingleton(new ToolsFactory());
        }
    }
}
