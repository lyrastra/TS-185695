using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.ExecutionContext.Abstractions.Exceptions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Jwt.Abstractions;
using Moedelo.Common.Jwt.Abstractions.Exceptions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;
using Moedelo.Common.ExecutionContext.Abstractions.Enums;

namespace Moedelo.Common.ExecutionContext
{
    [InjectAsSingleton(typeof(IExecutionInfoContextInitializer))]
    internal sealed class ExecutionInfoContextInitializer : IExecutionInfoContextInitializer
    {
        private readonly IJwtService jwtService;

        public ExecutionInfoContextInitializer(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        public ExecutionInfoContext Initialize(string token, ExecutionInfoContextTokenDecodingOptions options)
        {
            ExecutionContextClaims claims = null;

            try
            {
                claims = jwtService.Decode<ExecutionContextClaims>(token);
            }
            catch (JwtException exception)
            {
                throw new ExecutionContextInitializationFailedException(
                    ExecutionContextInitializationError.JwtDecodingFailed,
                    exception.Message,
                    exception);
            }

            if (claims.ExpirationDate <= DateTime.Now && !options.IgnoreOutdating)
            {
                throw new ExecutionContextInitializationFailedException(
                    ExecutionContextInitializationError.OutdatedClaims,
                    "Claims are outdated");
            }

            return new ExecutionInfoContext
            {
                FirmId = (FirmId) claims.FirmId,
                UserId = (UserId) claims.UserId,
                RoleId = (RoleId) claims.RoleId,
                Scopes = claims.Scopes != null
                    ? new HashSet<string>(claims.Scopes)
                    : Array.Empty<string>(),
                UserRules = claims.UserRules != null
                    ? new HashSet<AccessRule>(claims.UserRules)
                    : Array.Empty<AccessRule>()
            };
        }

        public ExecutionInfoContext Initialize(string token)
        {
            return Initialize(token, default);
        }
    }
}