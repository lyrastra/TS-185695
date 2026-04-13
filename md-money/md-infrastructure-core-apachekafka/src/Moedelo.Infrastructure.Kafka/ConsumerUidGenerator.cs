using System;
using System.Diagnostics;

namespace Moedelo.Infrastructure.Kafka;

public static class ConsumerUidGenerator
{
    private static readonly Lazy<int> CurrentProcessId = new (() => Process.GetCurrentProcess().Id);

    public static string NextUid => $"{Environment.MachineName}--{CurrentProcessId.Value}--{Guid.NewGuid()}";
}
