using System;

namespace Moedelo.InfrastructureV2.Domain.Exceptions.ApacheKafka;

public class SendMessageException : Exception
{
    public SendMessageException(string message) : base(message)
    {
    }
}