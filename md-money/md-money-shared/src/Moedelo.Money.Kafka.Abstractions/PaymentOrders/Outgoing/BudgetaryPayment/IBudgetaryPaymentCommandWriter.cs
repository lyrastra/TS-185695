using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentCommandWriter
    {
        Task WriteImportAsync(ImportBudgetaryPayment commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateBudgetaryPayment commandData);
    }
}