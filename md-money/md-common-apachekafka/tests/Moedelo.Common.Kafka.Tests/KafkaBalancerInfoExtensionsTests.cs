using FluentAssertions;
using Moedelo.Common.Kafka.Extensions;
using Moedelo.Common.Kafka.Models;

namespace Moedelo.Common.Kafka.Tests;

[TestFixture(Description = "Тесты на метод GetRebalanceRequirements")]
[Parallelizable(ParallelScope.All)]
public class KafkaBalancerInfoExtensionsGetRebalanceRequirementsTests
{
    [Test(Description = "Возращает не пустое значение, если на входе null")]
    public void ReturnsNotEmpty_IfNull()
    {
        const int runningConsumerCount = 8;
        var state = default(ServiceKafkaBalanceState);
        var requirements = state.GetRebalanceRequirements(runningConsumerCount);
        
        requirements.IsEmpty().Should().BeFalse();
    }

    [Test(Description =
        "Возращает значение со статусом Granted по количеству запущенных консьюмеров, если на входе null")]
    public void ReturnsGrantedWithConsumersCount_ForNull()
    {
        const int runningConsumerCount = 8;
        var state = default(ServiceKafkaBalanceState);
        var requirements = state.GetRebalanceRequirements(runningConsumerCount);

        var expected = new KafkaRebalanceRequirements
        {
            Quota = runningConsumerCount,
            RunningCount = runningConsumerCount,
            StopCount = 0,
            StartNewCount = 0,
            NeedToSetWorkingStatus = true
        };

        requirements.Should().BeEquivalentTo(expected);
    }

    [Test(Description = "Возвращает пустое значение, если квота не задана")]
    public void ReturnsEmpty_IfQuotaIsNull(
#pragma warning disable CS0618
        [Values(StatusWatchConsumer.Created, StatusWatchConsumer.Working, StatusWatchConsumer.Granted)]
#pragma warning restore CS0618
        StatusWatchConsumer status)
    {
        var state = new ServiceKafkaBalanceState
        {
            Status = status,
            Quota = null
        };

        var requirements = state.GetRebalanceRequirements(runningConsumerCount: 10);
        requirements.IsEmpty().Should().BeTrue();
    }
    
    [Test(Description = "Копирует значение квоты в результат")]
    public void Copies_QuotaValueFromInput(
        [Random(1, 12, 4)]
        int quota)
    {
        var state = new ServiceKafkaBalanceState
        {
            Status = StatusWatchConsumer.Granted,
            Quota = quota
        };

        var requirements = state.GetRebalanceRequirements(runningConsumerCount: 10);
        requirements.Quota.Should().Be(quota);
    }

    [Test(Description = "Копирует значение запущенных консьюмеров в результат")]
    public void Copies_RunningConsumerCountValueFromInput(
        [Random(1, 12, 4)]
        int runningConsumerCount)
    {
        var state = new ServiceKafkaBalanceState
        {
            Status = StatusWatchConsumer.Granted,
            Quota = 3
        };

        var requirements = state.GetRebalanceRequirements(runningConsumerCount);
        requirements.RunningCount.Should().Be(runningConsumerCount);
    }

    [Test(Description = "Корректно рассчитывает необходимое количество новых консьюмеров")]
    [TestCase(9, 0, ExpectedResult = 9)]
    [TestCase(12, 9, ExpectedResult = 3)]
    [TestCase(12, 12, ExpectedResult = 0)]
    [TestCase(6, 12, ExpectedResult = 0)]
    public int CalculateStartNewCountProperly(int quota, int runningConsumerCount)
    {
        var state = new ServiceKafkaBalanceState
        {
            Status = StatusWatchConsumer.Granted,
            Quota = quota
        };

        var requirements = state.GetRebalanceRequirements(runningConsumerCount);
        return requirements.StartNewCount;
    }

    [Test(Description = "Корректно рассчитывает количество консьюмеров, которые должны быть остановлены")]
    [TestCase(9, 0, ExpectedResult = 0)]
    [TestCase(12, 9, ExpectedResult = 0)]
    [TestCase(12, 12, ExpectedResult = 0)]
    [TestCase(6, 10, ExpectedResult = 4)]
    public int CalculateStopCountProperly(int quota, int runningConsumerCount)
    {
        var state = new ServiceKafkaBalanceState
        {
            Status = StatusWatchConsumer.Granted,
            Quota = quota
        };

        var requirements = state.GetRebalanceRequirements(runningConsumerCount);
        return requirements.StopCount;
    }

    [Test(Description = "Корректно выставляет NeedToSetWorkingStatus")]
    [TestCase(1, 0, StatusWatchConsumer.Granted, ExpectedResult = true)]
    [TestCase(1, 1, StatusWatchConsumer.Granted, ExpectedResult = true, Description = "Изменений не требуется, но статус надо выставить")]
    [TestCase(1, 1, StatusWatchConsumer.Working, ExpectedResult = true, Description = "Изменений нет, но надо сбросить квоту в null")]
    [TestCase(null, 1, StatusWatchConsumer.Working, ExpectedResult = false, Description = "Изменений нет, но надо сбросить квоту в null")]
    public bool SetsNeedToSetWorkingStatusProperly(int? quota, int runningConsumerCount, StatusWatchConsumer status)
    {
        var state = new ServiceKafkaBalanceState
        {
            Status = status,
            Quota = quota
        };

        var requirements = state.GetRebalanceRequirements(runningConsumerCount);
        return requirements.NeedToSetWorkingStatus;
    }
}
