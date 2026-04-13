using System;

namespace Moedelo.Infrastructure.Redis.Abstractions.Models;

public struct DistributedLockRunResult
{
    public bool Success { get; set; }
    public int AttemptCount { get; set; }
    public TimeSpan AwaitingBeforeInvoke { get; set; }
}
