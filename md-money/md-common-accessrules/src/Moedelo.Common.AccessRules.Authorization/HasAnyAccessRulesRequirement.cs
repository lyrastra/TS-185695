using Microsoft.AspNetCore.Authorization;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Common.AccessRules.Authorization
{
    public sealed class HasAnyAccessRulesRequirement : IAuthorizationRequirement
    {
        public AccessRule[] Rules { get; set; }

        public HasAnyAccessRulesRequirement(AccessRule[] rules)
        {
            Rules = rules;
        }
    }
}