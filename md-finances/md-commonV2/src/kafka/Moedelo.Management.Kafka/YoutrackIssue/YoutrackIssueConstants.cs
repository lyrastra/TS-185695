namespace Moedelo.Management.Kafka.YoutrackIssue
{
    public class YoutrackIssueConstants
    {
        public const string EntityName = "Issue";

        public static class Event
        {
            public static readonly string Topic = $"Management.Youtrack.Command.{EntityName}.Create";
        }
    }
}