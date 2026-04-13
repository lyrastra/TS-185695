namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders
{
    public enum UnknownCommandTypeStrategy
    {
        Undef = 0,
        Skip = 1,
        Throw = 2,
    }
}