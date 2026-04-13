using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Exceptions
{
    public class SendMessageException : Exception
    {
        public SendMessageException(string message) : base(message)
        {
        }
    }
}