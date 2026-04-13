using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Common.AccessRules.Authorization
{
    public sealed class HasAnyAccessRulesAttribute : TypeFilterAttribute
    {
        public HasAnyAccessRulesAttribute(params AccessRule[] rules) : base(typeof(HasAnyAccessRulesFilter))
        {
            Arguments = new object[] {new HasAnyAccessRulesRequirement(rules)};
            Order = int.MinValue;
        }
    }
}