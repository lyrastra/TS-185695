using System;
using System.Collections.Generic;
using FluentAssertions;
using Moedelo.CommonV2.Audit.Writers.Kafka.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moq;
using NUnit.Framework;

namespace Moedelo.CommonV2.Audit.Implementations.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class MapToKafkaModelValueTests
{
    [Test]
    [Description("Duration вычисляется корректно для нормальных значений")]
    public void MapToKafkaModelValue_Duration_CalculatesCorrectly()
    {
        // Arrange
        var startDate = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var finishDate = startDate.AddMilliseconds(12345); // 12.345 секунд
        var span = CreateMockSpan(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = span.MapToKafkaModelValue(utcNow);

        // Assert
        result.Duration.Should().Be(12345);
    }

    [Test]
    [Description("Duration ограничивается до int.MaxValue при переполнении (> 24.8 дней)")]
    public void MapToKafkaModelValue_Duration_ClampsToIntMaxValue_WhenOverflow()
    {
        // Arrange
        var startDate = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        // Разница больше 24.8 дней (int.MaxValue миллисекунд = 2,147,483,647 мс)
        var finishDate = startDate.AddMilliseconds(int.MaxValue + 1000L);
        var span = CreateMockSpan(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = span.MapToKafkaModelValue(utcNow);

        // Assert
        result.Duration.Should().Be(int.MaxValue);
    }

    [Test]
    [Description("Duration устанавливается в 0 при отрицательных значениях (FinishDateUtc < StartDateUtc)")]
    public void MapToKafkaModelValue_Duration_ClampsToZero_WhenNegative()
    {
        // Arrange
        var startDate = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var finishDate = startDate.AddMilliseconds(-1000); // Отрицательная разница
        var span = CreateMockSpan(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = span.MapToKafkaModelValue(utcNow);

        // Assert
        result.Duration.Should().Be(0);
    }

    [Test]
    [Description("Duration устанавливается в int.MaxValue при точном переполнении")]
    public void MapToKafkaModelValue_Duration_ClampsToIntMaxValue_WhenExactOverflow()
    {
        // Arrange
        var startDate = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var finishDate = startDate.AddMilliseconds(int.MaxValue);
        var span = CreateMockSpan(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = span.MapToKafkaModelValue(utcNow);

        // Assert
        result.Duration.Should().Be(int.MaxValue);
    }

    [Test]
    [Description("Duration устанавливается в 0 при одинаковых датах")]
    public void MapToKafkaModelValue_Duration_IsZero_WhenDatesAreEqual()
    {
        // Arrange
        var startDate = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var finishDate = startDate; // Одинаковые даты
        var span = CreateMockSpan(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = span.MapToKafkaModelValue(utcNow);

        // Assert
        result.Duration.Should().Be(0);
    }

    [Test]
    [Description("Duration корректно обрабатывает очень большие значения (близкие к int.MaxValue)")]
    public void MapToKafkaModelValue_Duration_HandlesLargeValues()
    {
        // Arrange
        var startDate = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        // Разница близка к int.MaxValue, но не превышает
        var finishDate = startDate.AddMilliseconds(int.MaxValue - 1);
        var span = CreateMockSpan(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = span.MapToKafkaModelValue(utcNow);

        // Assert
        result.Duration.Should().Be(int.MaxValue - 1);
    }

    private static IAuditSpan CreateMockSpan(
        DateTimeOffset? startDateUtc = null,
        DateTimeOffset? finishDateUtc = null)
    {
        var startDate = startDateUtc ?? new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var finishDate = finishDateUtc ?? startDate.AddMilliseconds(1000);

        var contextMock = new Mock<IAuditSpanContext>();
        contextMock.Setup(x => x.AsyncTraceId).Returns(Guid.NewGuid());
        contextMock.Setup(x => x.TraceId).Returns(Guid.NewGuid());
        contextMock.Setup(x => x.ParentId).Returns((Guid?)null);
        contextMock.Setup(x => x.CurrentId).Returns(Guid.NewGuid());
        contextMock.Setup(x => x.CurrentDepth).Returns((short)0);

        var spanMock = new Mock<IAuditSpan>();
        spanMock.Setup(x => x.Context).Returns(contextMock.Object);
        spanMock.Setup(x => x.Type).Returns(AuditSpanType.InternalCode);
        spanMock.Setup(x => x.AppName).Returns("TestApp");
        spanMock.Setup(x => x.Name).Returns("TestSpan");
        spanMock.Setup(x => x.IsNameNormalized).Returns(false);
        spanMock.Setup(x => x.StartDateUtc).Returns(startDate);
        spanMock.Setup(x => x.FinishDateUtc).Returns(finishDate);
        spanMock.Setup(x => x.HasError).Returns(false);
        spanMock.Setup(x => x.Tags).Returns(new Dictionary<string, List<object>>());

        return spanMock.Object;
    }
}
