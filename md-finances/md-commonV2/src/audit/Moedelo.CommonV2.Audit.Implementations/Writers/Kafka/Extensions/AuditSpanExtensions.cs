using System;
using Moedelo.CommonV2.Audit.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.CommonV2.Audit.Writers.Kafka.Extensions;

internal static class AuditSpanExtensions
{
    private static readonly string HostName = Environment.MachineName;

    internal static AuditSpanKafkaModel MapToKafkaModelValue(this IAuditSpan row, DateTime utcNow)
    {
        const int maxTagsJsonSize = 1024 * 64;

        return new AuditSpanKafkaModel
        {
            Metadata = new KafkaMessageValueMetadata
            {
                MessageId = Guid.NewGuid(),
                MessageDate = utcNow,
            },
            AsyncTraceId = row.Context.AsyncTraceId,
            TraceId = row.Context.TraceId,
            ParentId = row.Context.ParentId,
            CurrentId = row.Context.CurrentId,
            CurrentDepth = row.Context.CurrentDepth,
            App = row.AppName,
            Name = row.GetNormalizedName(),
            Type = (byte)row.Type,
            IsError = row.HasError,
            StartDateUtc = DateTime.SpecifyKind(row.StartDateUtc.DateTime, DateTimeKind.Utc),
            EndDateUtc = DateTime.SpecifyKind(row.FinishDateUtc.DateTime, DateTimeKind.Utc),
            // Предотвращаем переполнение int32: если TotalMilliseconds > int.MaxValue, устанавливаем int.MaxValue
            // Также защищаемся от отрицательных значений (если FinishDateUtc < StartDateUtc)
            // Ограничение до int.MaxValue необходимо для совместимости с ClickHouse (таблица span использует UInt32)
            Duration = (long)Math.Min(Math.Max((row.FinishDateUtc - row.StartDateUtc).TotalMilliseconds, 0), int.MaxValue),
            Host = HostName,
            TagsJson = row.Tags.TagsToJson(maxTagsJsonSize)
        };
    }
}