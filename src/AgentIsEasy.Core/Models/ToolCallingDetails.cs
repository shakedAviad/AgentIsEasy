using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentIsEasy.Core.Models;

public class ToolCallingDetails
{
    public required FunctionInvocationContext Context { get; set; }   
    public override string ToString()
    {
        var toolDetails = new StringBuilder($"- Tool Call: '{Context.Function.Name}'");
        
        if (Context.Arguments.Any())
        {
            toolDetails.AppendLine($" (Args: {string.Join(",", Context.Arguments.Select(x => $"[{x.Key} = {x.Value}]"))}");
        }

        return toolDetails.ToString();
    }
}
