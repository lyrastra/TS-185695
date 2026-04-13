using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.Abstractions.Helpers
{
    public static class AuditSpanBuilderExtensions
    {
        public static IAuditSpanBuilder TagCodeSourcePath(
            this IAuditSpanBuilder spanBuilder,
            string memberName,
            string sourceFilePath,
            int sourceLineNumber)
        {
            if (spanBuilder.IsEnabled)
            {
                spanBuilder.WithTag("SourceCode",
                    $"{sourceFilePath.NormalizeSourceFilePath()}@{sourceLineNumber} (func {memberName})");
            }

            return spanBuilder;
        }

        public static async Task RunAsync(
            this IAuditSpanBuilder builder,
            Func<Task> runInsideScope)
        {
            using var scope = builder.Start();

            try
            {
                await runInsideScope.Invoke().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        public static async Task RunAsync<TArg1>(
            this IAuditSpanBuilder builder,
            Func<TArg1, Task> runInsideScope,
            TArg1 arg1)
        {
            using var scope = builder.Start();

            try
            {
                await runInsideScope.Invoke(arg1).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        public static async Task RunAsync<TArg1, TArg2>(
            this IAuditSpanBuilder builder,
            Func<TArg1, TArg2, Task> runInsideScope,
            TArg1 arg1, TArg2 arg2)
        {
            using var scope = builder.Start();

            try
            {
                await runInsideScope.Invoke(arg1, arg2).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        public static async Task RunAsync<TArg1, TArg2>(
            this IAuditSpanBuilder builder,
            Func<TArg1, TArg2, CancellationToken, Task> runInsideScope,
            TArg1 arg1, TArg2 arg2,
            CancellationToken cancellationToken)
        {
            using var scope = builder.Start();

            try
            {
                await runInsideScope.Invoke(arg1, arg2, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        public static async Task<TResult> RunAsync<TResult>(
            this IAuditSpanBuilder builder,
            Func<Task<TResult>> runInsideScope)
        {
            using var scope = builder.Start();

            try
            {
                return await runInsideScope.Invoke();
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        public static async Task<TResult> RunAsync<TResult>(
            this IAuditSpanBuilder builder,
            Func<IAuditSpan, Task<TResult>> runInsideScope)
        {
            using var scope = builder.Start();

            try
            {
                return await runInsideScope.Invoke(scope.Span);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        public static async Task<TResult> RunAsync<TResult, TArg1>(
            this IAuditSpanBuilder builder,
            Func<TArg1, IAuditSpan, Task<TResult>> runInsideScope,
            TArg1 arg1)
        {
            using var scope = builder.Start();

            try
            {
                return await runInsideScope.Invoke(arg1, scope.Span);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        public static async Task<TResult> RunAsync<TResult, TArg1>(
            this IAuditSpanBuilder builder,
            Func<TArg1, IAuditSpan, CancellationToken, Task<TResult>> runInsideScope,
            TArg1 arg1,
            CancellationToken cancellationToken)
        {
            using var scope = builder.Start();

            try
            {
                return await runInsideScope.Invoke(arg1, scope.Span, cancellationToken);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }
    }
}
