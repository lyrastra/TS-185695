using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentCommandWriter
    {
        Task WriteImportAsync(ImportUnifiedBudgetaryPayment commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateUnifiedBudgetaryPayment commandData);
    }
}