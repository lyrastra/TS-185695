namespace Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders
{
    public enum UnknownEventTypeStrategy
    {
        Undef = 0,
        Skip = 1,
        Throw = 2,
    }
}