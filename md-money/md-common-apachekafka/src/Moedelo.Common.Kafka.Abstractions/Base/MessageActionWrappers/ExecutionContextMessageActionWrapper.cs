#nullable enable
using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers
{
    public sealed class ExecutionContextMessageActionWrapper<TMessage> : IMessageActionWrapper<TMessage>
        where TMessage : MoedeloKafkaMessageValueBase
    {
        private readonly IExecutionInfoContextInitializer contextInitializer;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly bool ignoreExecutionContextOutdating;
        private readonly Func<TMessage, Exception, ValueTask<string>>? executionContextTokenFallback;

        public ExecutionContextMessageActionWrapper(IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            bool ignoreExecutionContextOutdating,
            Func<TMessage, Exception, ValueTask<string>>? executionContextTokenFallback)
        {
            this.contextInitializer = contextInitializer;
            this.contextAccessor = contextAccessor;
            this.ignoreExecutionContextOutdating = ignoreExecutionContextOutdating;
            this.executionContextTokenFallback = executionContextTokenFallback;
        }

        public Func<TMessage, Task> Wrap(Func<TMessage, Task> onMessage)
        {
            return async message =>
            {
                var (token, context) = await GetExecutionContextAsync(message);

                await contextAccessor.RunInContextAsync(token, context, onMessage, message);
            };
        }

        private readonly record struct ExecutionContext(string Token, ExecutionInfoContext Context);
        
        private async ValueTask<ExecutionContext> GetExecutionContextAsync(TMessage message)
        {
            var decodingOptions = new ExecutionInfoContextTokenDecodingOptions
            {
                IgnoreOutdating = ignoreExecutionContextOutdating
            };

            var token = message.Token;

            try
            {
                return new (token, contextInitializer.Initialize(token, decodingOptions));
            }
            catch (Exception exception) when (executionContextTokenFallback is not null)
            {
                token = await executionContextTokenFallback(message, exception);

                if (token == null)
                {
                    throw;
                }
            }
            
            return new (token, contextInitializer.Initialize(token, decodingOptions));
        }
    }
}