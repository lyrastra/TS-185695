using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Helpers;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Redis.Abstractions.Extensions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.Common.Redis.Abstractions
{
    public abstract partial class MoedeloRedisDbExecutorBase
    {
        private async Task<TResult> RunInAuditScope<TResult>(
            IReadOnlyCollection<string> keys,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisDbExecuter, IRedisConnection, IReadOnlyCollection<string>, Task<TResult>> action,
            [CallerMemberName] string memberName = "")
        {
            var connection = GetConnection();
            var spanName =
                $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKeys = keys.Select(GetKey).ToArray();

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTime.UtcNow)
                .WithConnection(connection)
                .WithKeys(realKeys)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                var result = await action(redisExecutor, connection, realKeys).ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        private async Task<TResult> RunInAuditScope<T, TResult>(
            string key,
            T parameters,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisDbExecuter, IRedisConnection, string, T, Task<TResult>> action,
            [CallerMemberName] string memberName = "") where T : struct
        {
            var connection = GetConnection();
            var spanName =
                $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKey = GetKey(key);

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTime.UtcNow)
                .WithConnection(connection)
                .WithKey(realKey)
                .WithParams(parameters)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                var result = await action(redisExecutor, connection, realKey, parameters).ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        private async Task RunInAuditScope<T>(
            string key,
            T parameters,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisDbExecuter, IRedisConnection, string, T, Task> action,
            [CallerMemberName] string memberName = "") where T : struct
        {
            var connection = GetConnection();
            var spanName =
                $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKey = GetKey(key);

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTime.UtcNow)
                .WithConnection(connection)
                .WithKey(realKey)
                .WithParams(parameters)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                await action(redisExecutor, connection, realKey, parameters).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        private async Task<TResult> RunInAuditScope<TResult>(
            string key,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisDbExecuter, IRedisConnection, string, Task<TResult>> action,
            [CallerMemberName] string memberName = "")
        {
            var connection = GetConnection();
            var spanName =
                $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKey = GetKey(key);

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTime.UtcNow)
                .WithConnection(connection)
                .WithKey(realKey)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                var result = await action(redisExecutor, connection, realKey).ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }
    }
}
