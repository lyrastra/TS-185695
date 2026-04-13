using System;
using System.Collections.Generic;
using FluentAssertions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Audit.Writers.Kafka.Extensions;
using Moq;

namespace Moedelo.Common.Audit.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class MapToKafkaMessageValueTests
{
    [Test]
    [Description("Duration вычисляется корректно для нормальных значений")]
    public void MapToKafkaMessageValue_Duration_CalculatesCorrectly()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var finishDate = startDate.AddMilliseconds(12345); // 12.345 секунд
        var spanData = CreateMockSpanData(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Duration.Should().Be(12345);
    }

    [Test]
    [Description("Duration ограничивается до int.MaxValue при переполнении (> 24.8 дней)")]
    public void MapToKafkaMessageValue_Duration_ClampsToIntMaxValue_WhenOverflow()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        // Разница больше 24.8 дней (int.MaxValue миллисекунд = 2,147,483,647 мс)
        var finishDate = startDate.AddMilliseconds(int.MaxValue + 1000L);
        var spanData = CreateMockSpanData(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Duration.Should().Be(int.MaxValue);
    }

    [Test]
    [Description("Duration устанавливается в 0 при отрицательных значениях (FinishDateUtc < StartDateUtc)")]
    public void MapToKafkaMessageValue_Duration_ClampsToZero_WhenNegative()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var finishDate = startDate.AddMilliseconds(-1000); // Отрицательная разница
        var spanData = CreateMockSpanData(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Duration.Should().Be(0);
    }

    [Test]
    [Description("Duration устанавливается в int.MaxValue при точном переполнении")]
    public void MapToKafkaMessageValue_Duration_ClampsToIntMaxValue_WhenExactOverflow()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var finishDate = startDate.AddMilliseconds(int.MaxValue);
        var spanData = CreateMockSpanData(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Duration.Should().Be(int.MaxValue);
    }

    [Test]
    [Description("Duration устанавливается в 0 при одинаковых датах")]
    public void MapToKafkaMessageValue_Duration_IsZero_WhenDatesAreEqual()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var finishDate = startDate; // Одинаковые даты
        var spanData = CreateMockSpanData(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Duration.Should().Be(0);
    }

    [Test]
    [Description("Duration корректно обрабатывает очень большие значения (близкие к int.MaxValue)")]
    public void MapToKafkaMessageValue_Duration_HandlesLargeValues()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        // Разница близка к int.MaxValue, но не превышает
        var finishDate = startDate.AddMilliseconds(int.MaxValue - 1);
        var spanData = CreateMockSpanData(startDate, finishDate);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Duration.Should().Be(int.MaxValue - 1);
    }

    [Test]
    [Description("Host заполняется из spanData, если указан")]
    public void MapToKafkaMessageValue_Host_UsesSpanDataHost_WhenProvided()
    {
        // Arrange
        const string customHost = "custom-host-name";
        var spanData = CreateMockSpanData(host: customHost);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Host.Should().Be(customHost);
    }

    [Test]
    [Description("Host заполняется из Environment.MachineName, если не указан в spanData")]
    public void MapToKafkaMessageValue_Host_UsesMachineName_WhenNotProvided()
    {
        // Arrange
        var spanData = CreateMockSpanData(host: null);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Host.Should().Be(Environment.MachineName);
    }

    [Test]
    [Description("Host заполняется из Environment.MachineName, если указана пустая строка")]
    public void MapToKafkaMessageValue_Host_UsesMachineName_WhenEmptyString()
    {
        // Arrange
        var spanData = CreateMockSpanData(host: string.Empty);
        var utcNow = DateTime.UtcNow;

        // Act
        var result = spanData.MapToKafkaMessageValue(utcNow);

        // Assert
        result.Host.Should().Be(Environment.MachineName);
    }

    private static IAuditSpanData CreateMockSpanData(
        DateTime? startDateUtc = null,
        DateTime? finishDateUtc = null,
        string? host = null)
    {
        var startDate = startDateUtc ?? new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var finishDate = finishDateUtc ?? startDate.AddMilliseconds(1000);

        var contextMock = new Mock<IAuditSpanContext>();
        contextMock.Setup(x => x.AsyncTraceId).Returns(Guid.NewGuid());
        contextMock.Setup(x => x.TraceId).Returns(Guid.NewGuid());
        contextMock.Setup(x => x.ParentId).Returns((Guid?)null);
        contextMock.Setup(x => x.CurrentId).Returns(Guid.NewGuid());
        contextMock.Setup(x => x.CurrentDepth).Returns((short)0);

        var spanDataMock = new Mock<IAuditSpanData>();
        spanDataMock.Setup(x => x.Context).Returns(contextMock.Object);
        spanDataMock.Setup(x => x.Type).Returns(AuditSpanType.InternalCode);
        spanDataMock.Setup(x => x.Host).Returns(host!);
        spanDataMock.Setup(x => x.AppName).Returns("TestApp");
        spanDataMock.Setup(x => x.Name).Returns("TestSpan");
        spanDataMock.Setup(x => x.StartDateUtc).Returns(startDate);
        spanDataMock.Setup(x => x.FinishDateUtc).Returns(finishDate);
        spanDataMock.Setup(x => x.HasError).Returns(false);
        spanDataMock.Setup(x => x.Tags).Returns(new Dictionary<string, List<object>>());

        // Настройка GetNormalizedName() через расширение
        spanDataMock.Setup(x => x.Name).Returns("TestSpan");

        return spanDataMock.Object;
    }
}
