using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount
{
    public interface IRefundToSettlementAccountCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IRefundToSettlementAccountCommandReaderBuilder OnImport(Func<ImportRefundToSettlementAccount, Task> onCommand);
        IRefundToSettlementAccountCommandReaderBuilder OnImportDuplicate(Func<ImportRefundToSettlementAccountDuplicate, Task> onCommand);
        IRefundToSettlementAccountCommandReaderBuilder OnImportWithMissingContractor(Func<ImportRefundToSettlementAccountWithMissingContragent, Task> onCommand);
    }
}
