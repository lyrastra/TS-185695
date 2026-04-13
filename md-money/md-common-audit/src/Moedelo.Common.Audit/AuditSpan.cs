using System;
using System.Collections.Generic;
using System.Diagnostics;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Audit;

internal sealed class AuditSpan : IAuditSpan
{
    private static readonly string HostName = Environment.MachineName;

    private readonly AuditTracer tracer;
    private readonly Dictionary<string, List<object>> tags = new();
    private readonly Stopwatch sw;
    private bool finished;

    public AuditSpan(
        AuditTracer tracer,
        IAuditSpanContext context,
        AuditSpanType type,
        string appName,
        string name,
        DateTime startDateUtc)
    {
        this.tracer = tracer;
        Context = context;
        Type = type;
        AppName = appName;
        Name = name;
        StartDateUtc = startDateUtc;
        sw = Stopwatch.StartNew();
    }

    public IAuditSpanContext Context { get; }

    public AuditSpanType Type { get; }

    public string Host => HostName;

    public string AppName { get; }

    public string Name { get; private set; }

    public DateTime StartDateUtc { get; }

    public DateTime FinishDateUtc { get; private set; }

    public bool HasError { get; private set; }

    public IReadOnlyDictionary<string, List<object>> Tags => tags;

    public void AddTag(string tagName, object tagValue)
    {
        lock (tags)
        {
            EnsureIsNotFinished("set tag to already finished span");
            AddTagInternal(tagName, tagValue);
        }
    }

    public void SetError()
    {
        lock (tags)
        {
            EnsureIsNotFinished("set error to already finished span");
            HasError = true;
        }
    }

    public void SetError(Exception exception)
    {
        lock (tags)
        {
            EnsureIsNotFinished("set error to already finished span");
            HasError = true;
            AddTagInternal("Exception", GetExceptionInfo(exception));
        }
    }

    private static object GetExceptionInfo(Exception exception)
    {
        try
        {
            return new
            {
                Message = exception.Message,
                DemystifiedStacktrace = exception.ToStringDemystified()
            };
        }
        catch
        {
            return exception.ToString();
        }
    }

    public void Finish()
    {
        Finish(DateTime.UtcNow);
    }

    public void Finish(DateTime finishDateUtc)
    {
        lock (tags)
        {
            EnsureIsNotFinished("finish already finished span");

            sw.Stop();
            FinishDateUtc = StartDateUtc.Add(sw.Elapsed);

            if (finishDateUtc > FinishDateUtc)
            {
                FinishDateUtc = finishDateUtc;
            }

            finished = true;

            tracer.AddFinishedSpan(this);
        }
    }

    public void SetName(string value)
    {
        if (string.IsNullOrEmpty(value) == false)
        {
            Name = value;
        }
    }

    private void EnsureIsNotFinished(string errorMessage)
    {
        if (finished)
        {
            throw new InvalidOperationException(errorMessage);
        }
    }

    private void AddTagInternal(string tagName, object tagValue)
    {
        if (tags.ContainsKey(tagName) == false)
        {
            tags.Add(tagName, []);
        }

        tags[tagName].Add(tagValue);
    }
}
