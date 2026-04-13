using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Konragents.Kafka.Abstractions.KontragentDebt.Commands;

namespace Moedelo.Konragents.Kafka.Abstractions.KontragentDebt
{
    public interface IKontragentDebtCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IKontragentDebtCommandReaderBuilder OnRecalculate(Func<KontragentDebtRecalculationCommand, Task> onCommand);
    }
}