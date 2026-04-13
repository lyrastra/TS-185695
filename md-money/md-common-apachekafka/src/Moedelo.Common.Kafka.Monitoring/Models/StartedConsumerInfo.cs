namespace Moedelo.Common.Kafka.Monitoring.Models;

internal record struct StartedConsumerInfo(string Topic, string Host, long Pid, string At)
{
    internal static StartedConsumerInfo Create(string topic) => new(
        topic,
        Environment.MachineName,
        Environment.ProcessId,
        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zz"));
}