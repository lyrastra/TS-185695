using System.Linq;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Common.ExecutionContext.Abstractions.Models;

public static class ExecutionInfoContextExtensions
{
    public static bool HasAllRules(this ExecutionInfoContext context, params AccessRule[] rules)
    {
        return rules.All(r => context.UserRules.Contains(r));
    }
        
    public static bool HasAnyRule(this ExecutionInfoContext context, AccessRule rule)
    {
        return context.UserRules.Contains(rule);
    }

    public static bool HasAnyRule(this ExecutionInfoContext context, params AccessRule[] rules)
    {
        return rules.Any(r => context.UserRules.Contains(r));
    }
}
