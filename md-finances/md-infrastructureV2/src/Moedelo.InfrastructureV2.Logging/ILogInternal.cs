namespace Moedelo.InfrastructureV2.Logging
{
    internal interface ILogInternal
    {
        bool IsLevelEnabled(LogLevel level);

        void WriteEvent(LogLevel level, string logEvent);
    }
}
