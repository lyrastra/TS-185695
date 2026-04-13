using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IMediationFeeCommandReaderBuilder OnImport(Func<ImportMediationFee, Task> onCommand);
        IMediationFeeCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateMediationFee, Task> onCommand);
        IMediationFeeCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractMediationFee, Task> onCommand);
        IMediationFeeCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorMediationFee, Task> onCommand);
    }
}
