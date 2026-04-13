using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract
{
    public interface IAgencyContractCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IAgencyContractCommandReaderBuilder OnImport(Func<ImportAgencyContract, Task> onCommand);
        IAgencyContractCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateAgencyContract, Task> onCommand);
        IAgencyContractCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractAgencyContract, Task> onCommand);
        IAgencyContractCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorAgencyContract, Task> onCommand);
    }
}
