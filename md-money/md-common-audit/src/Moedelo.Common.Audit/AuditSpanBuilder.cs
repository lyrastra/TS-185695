using System;
using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Audit
{
    internal sealed class AuditSpanBuilder : IAuditSpanBuilder
    {
        private readonly AuditTracer tracer;

        private readonly IAuditScopeManager scopeManager;

        private readonly AuditSpanType type;

        private readonly string appName;
        
        private readonly string spanName;
        
        private bool ignoreTraceId;

        private IAuditSpanContext parentSpanContext;
        
        private DateTime startDateUtc = DateTime.MinValue;

        private readonly List<Tag> defaultTags = new List<Tag>();

        public AuditSpanBuilder(AuditTracer tracer, IAuditScopeManager scopeManager, AuditSpanType type, string appName, string spanName)
        {
            this.tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            this.scopeManager = scopeManager ?? throw new ArgumentNullException(nameof(scopeManager));
            this.type = type;
            
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            this.appName = appName;
            
            if (string.IsNullOrWhiteSpace(spanName))
            {
                throw new ArgumentNullException(nameof(spanName));
            }
            
            this.spanName = spanName;
        }

        public IAuditSpanBuilder AsChildOf(IAuditSpanContext spanContext)
        {
            parentSpanContext = spanContext;
            
            return this;
        }

        public IAuditSpanBuilder AsChildOf(IAuditSpan span)
        {
            return AsChildOf(span?.Context);
        }

        public IAuditSpanBuilder IgnoreTraceId()
        {
            ignoreTraceId = true;
            
            return this;
        }

        public IAuditSpanBuilder WithStartDateUtc(DateTime utc)
        {
            startDateUtc = utc;

            return this;
        }

        public IAuditSpanBuilder WithTag(string tagName, object tagValue)
        {
            defaultTags.Add(new Tag(tagName, tagValue));
            
            return this;
        }

        public IAuditScope Start()
        {   
            var span = Build();
            
            return scopeManager.StartScope(span);
        }

        public bool IsEnabled => true;

        private IAuditSpan Build()
        {
            var buildParentSpanContext = parentSpanContext ?? scopeManager.Current?.Span?.Context;

            var context = buildParentSpanContext == null
                ? AuditSpanContext.New()
                : AuditSpanContext.ChildOf(buildParentSpanContext, ignoreTraceId);

            if (startDateUtc == DateTime.MinValue)
            {
                startDateUtc = DateTime.UtcNow;
            }

            var span = new AuditSpan(tracer, context, type, appName, spanName, startDateUtc);

            foreach (var tag in defaultTags)
            {
                span.AddTag(tag.Key, tag.Value);
            }
            
            return span;
        }
        
        private class Tag
        {
            public Tag(string key, object value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; }

            public object Value { get; }
        }
    }
}