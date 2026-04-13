using System;
using System.Collections.Generic;
using System.Diagnostics;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.Audit
{
    public class AuditSpan : IAuditSpan
    {
        private readonly object lockObj = new object();
        
        private bool finished;
        
        private readonly AuditTracer tracer;

        private readonly Dictionary<string, List<object>> tags = new Dictionary<string, List<object>>();
        
        private readonly Stopwatch sw;
        
        public AuditSpan(
            AuditTracer tracer, 
            IAuditSpanContext context, 
            AuditSpanType type,
            string appName, 
            string name, 
            DateTimeOffset startDateUtc)
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

        public string AppName { get; }
        
        public string Name { get; private set; }

        public bool IsNameNormalized { get; private set; } = false;

        public DateTimeOffset StartDateUtc { get; }
        
        public DateTimeOffset FinishDateUtc { get; private set; }
        
        public bool HasError { get; private set; }

        public IReadOnlyDictionary<string, List<object>> Tags => tags;

        public void AddTag(string tagName, object tagValue)
        {
            lock (lockObj)
            {
                CheckForFinished("set tag to already finished span");
                AddTagInternal(tagName, tagValue);
            }
        }

        public void SetError()
        {
            lock (lockObj)
            {
                CheckForFinished("set error to already finished span");
                HasError = true;
            }
        }

        public void SetError(Exception ex)
        {
            lock (lockObj)
            {
                CheckForFinished("set error to already finished span");
                HasError = true;
                AddTagInternal("Exception", ex.ToString());
            }
        }
        
        public void Finish()
        {
            Finish(DateTimeOffset.UtcNow);
        }
        
        public void Finish(DateTimeOffset finishDateUtc)
        {
            lock (lockObj)
            {
                CheckForFinished("finish already finished span");
                
                sw.Stop();
                var elapsed = sw.Elapsed;
                FinishDateUtc = StartDateUtc.Add(elapsed);

                if (finishDateUtc > FinishDateUtc)
                {
                    FinishDateUtc = finishDateUtc;
                }
                
                finished = true;
                
                tracer.AddFinishedSpan(this);
            }
        }

        public void SetNormalizedName(string name)
        {
            Name = name;
            IsNameNormalized = true;
        }

        private void CheckForFinished(string format, params object[] args)
        {
            if (!finished)
            {
                return;
            }

            var ex = new InvalidOperationException(string.Format(format, args));
            throw ex;
        }

        private void AddTagInternal(string tagName, object tagValue)
        {
            if (tags.ContainsKey(tagName) == false)
            {
                tags.Add(tagName, new List<object>());
            }
            
            tags[tagName].Add(tagValue);
        }
    }
}