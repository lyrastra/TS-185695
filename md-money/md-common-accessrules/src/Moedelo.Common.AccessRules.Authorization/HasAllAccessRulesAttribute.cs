using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Common.AccessRules.Authorization
{
    public sealed class HasAllAccessRulesAttribute : TypeFilterAttribute
    {
        public HasAllAccessRulesAttribute(params AccessRule[] rules) : base(typeof(HasAllAccessRulesFilter))
        {
            Arguments = new object[] {new HasAllAccessRulesRequirement(rules)};
            Order = int.MinValue;
        }
    }
}