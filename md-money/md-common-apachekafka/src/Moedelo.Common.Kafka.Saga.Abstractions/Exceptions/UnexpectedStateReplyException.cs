using System;

namespace Moedelo.Common.Kafka.Saga.Abstractions.Exceptions
{
    public sealed class UnexpectedStateReplyException : Exception
    {
        public UnexpectedStateReplyException(string stateType, string replyType)
            : base($"Unexpected reply type {replyType} for state type {stateType}")
        {
        }
    }
}
