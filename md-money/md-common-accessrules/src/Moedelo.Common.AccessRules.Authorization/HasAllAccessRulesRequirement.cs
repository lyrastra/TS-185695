using Microsoft.AspNetCore.Authorization;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Common.AccessRules.Authorization
{
    public sealed class HasAllAccessRulesRequirement : IAuthorizationRequirement
    {
        public AccessRule[] Rules { get; set; }

        public HasAllAccessRulesRequirement(AccessRule[] rules)
        {
            Rules = rules;
        }
    }
}