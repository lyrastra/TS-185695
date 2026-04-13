namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    public enum UnexpectedSagaReplyReaction
    {
        PausePartition,
        LogErrorAndIgnore,
        SilentIgnore
    }
}
