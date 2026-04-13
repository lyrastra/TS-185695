using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IBankFeeCommandReaderBuilder OnImport(Func<ImportBankFee, Task> onCommand);
        IBankFeeCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateBankFee, Task> onCommand);
    }
}
