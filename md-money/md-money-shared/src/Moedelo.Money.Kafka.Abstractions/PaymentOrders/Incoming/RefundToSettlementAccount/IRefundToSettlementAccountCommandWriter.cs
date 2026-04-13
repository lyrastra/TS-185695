using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount
{
    public interface IRefundToSettlementAccountCommandWriter
    {
        Task WriteImportAsync(ImportRefundToSettlementAccount commandData);

        Task WriteImportDuplicateAsync(ImportRefundToSettlementAccountDuplicate commandData);

        Task WriteImportWithMissingContractorAsync(ImportRefundToSettlementAccountWithMissingContragent commandData);
    }
}
