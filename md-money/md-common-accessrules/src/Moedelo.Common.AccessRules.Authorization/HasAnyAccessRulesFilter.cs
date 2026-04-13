using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Moedelo.Common.AccessRules.Authorization
{
    public sealed class HasAnyAccessRulesFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly HasAnyAccessRulesRequirement requirement;

        public HasAnyAccessRulesFilter(
            IExecutionInfoContextAccessor contextAccessor,
            HasAnyAccessRulesRequirement requirement)
        {
            this.contextAccessor = contextAccessor;
            this.requirement = requirement;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var executionContext = contextAccessor.ExecutionInfoContext;

            var hasRules = executionContext != null &&
                requirement.Rules.Any(r => executionContext.UserRules.Contains(r));
            if (hasRules == false)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
            }

            return Task.CompletedTask;
        }
    }
}