using System;

namespace Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs;

public static class Topics
{
    private const string Domain = "HistoricalLog";

    public static class OperationLogs
    {
        public const string EntityName = "OperationLog";
            
        public static class Command
        {
            public static readonly string Topic = $"{Domain}.Command.{EntityName}";
        }
    }
        
    [Obsolete]
    public static class Commands
    {
        private const string Prefix = "HistoricalLogs.OperationLogs.Command";

        public static readonly string Log = $"{Prefix}.Log";
    }
}