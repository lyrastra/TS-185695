using System;
using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Extensions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Audit.Writers.Kafka.Extensions;

internal static class AuditSpanExtensions
{
    private static readonly string HostName = Environment.MachineName; 

    internal static AuditSpanKafkaModel MapToKafkaMessageValue(this IAuditSpanData row, DateTime utcNow)
    {
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
            StartDateUtc = row.StartDateUtc,
            EndDateUtc = row.FinishDateUtc,
            // Предотвращаем переполнение int32: если TotalMilliseconds > int.MaxValue, устанавливаем int.MaxValue
            // Также защищаемся от отрицательных значений (если FinishDateUtc < StartDateUtc)
            // Ограничение до int.MaxValue необходимо для совместимости с ClickHouse (таблица span использует UInt32)
            Duration = (long)Math.Min(Math.Max((row.FinishDateUtc - row.StartDateUtc).TotalMilliseconds, 0), int.MaxValue),
            Host = string.IsNullOrEmpty(row.Host) ? HostName : row.Host,
            TagsJson = row.Tags.TagsToJson()
        };
    }

    internal static string TagsToJson(this IReadOnlyDictionary<string, List<object>> spanTags)
    {
        try
        {
            return spanTags
                .ToJsonString(nullHandling: MdSerializerNullHandling.Ignore)
                .MaskSensitiveJsonFields();
        }
        catch(Exception e)
        {
            try
            {
                return new { ErrorOnJsonSerialization = e.Message }.ToJsonString();
            }
            catch
            {
                return "{\"Error\":\"Не удалось сериализовать ошибку сериализации\"}";
            }
        }
    }
}