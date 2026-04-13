namespace Moedelo.Common.Kafka.Monitoring.Models;

internal record struct ConsumerSetPartitionOnPauseInfo(string Topic, int Partition, string Host, long Pid, string At)
{
    internal static ConsumerSetPartitionOnPauseInfo Create(string topic, int partition) => new(
        topic,
        partition,
        Environment.MachineName,
        Environment.ProcessId,
        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zz"));
}
