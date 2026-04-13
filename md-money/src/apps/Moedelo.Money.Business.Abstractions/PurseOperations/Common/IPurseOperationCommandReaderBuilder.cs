using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Common.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PurseOperations.Common
{
    public interface IPurseOperationCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IPurseOperationCommandReaderBuilder OnCreate(Func<CreatePurseOperation, Task> onCommand);
    }
}