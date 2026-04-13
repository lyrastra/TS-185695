namespace Moedelo.Common.Kafka.Abstractions.Entities.Messages
{
    public enum UnknownMessageTypeStrategy
    {
        Undef = 0,
        Skip = 1,
        Throw = 2,
    }
}