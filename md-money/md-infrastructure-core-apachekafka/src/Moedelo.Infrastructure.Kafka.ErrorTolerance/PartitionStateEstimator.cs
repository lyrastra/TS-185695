using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

[InjectAsSingleton(typeof(IPartitionStateEstimator))]
internal sealed class PartitionStateEstimator : IPartitionStateEstimator
{
    public PartitionMemoryEstimation EstimateMemoryStatus(
        IErrorToleratedKafkaConsumerOptions options,
        PartitionMemoryState state,
        long currentOffset)
    {
        // эта проверка стоит первой, потому что она опирается на данные, собранные в рамках текущей сессии и не требует дополнительных проверок
        if (state.UniqueSkippedMessageKeysCount > options.MaxPausedMessageKeys)
        {
            return new PartitionMemoryEstimation(true,
                $"Количество уникальных ключей пропущенных сообщений ({state.UniqueSkippedMessageKeysCount}) превышает максимально допустимое значение {options.MaxPausedMessageKeys}");
        }

        if (state.OffsetMapDepth > options.MaxOffsetMapDepth)
        {
            if (state.CommittedOffset == default || currentOffset >= state.CommittedOffset.Offset + state.OffsetMapDepth)
            {
                // закоммиченное смещение неизвестно либо мы находимся в последней позиции памяти
                return new PartitionMemoryEstimation(true,
                    $"Расстояние от закомиченной позиции до текущей ({state.OffsetMapDepth}) превышает максимально допустимое значение {options.MaxOffsetMapDepth}");
            }
        }

        if (state.CommittedOffset != default)
        {
            var passed = DateTime.UtcNow - state.CommittedOffset.AtUtc;

            if (passed > options.ErrorToleranceTimeSpan)
            {
                return new PartitionMemoryEstimation(true,
                    $"От последнего известного коммита прошло времени больше ({passed:G}) максимально допустимого {options.ErrorToleranceTimeSpan:G}");
            }
        }

        return new PartitionMemoryEstimation(false, string.Empty);
    }
}
