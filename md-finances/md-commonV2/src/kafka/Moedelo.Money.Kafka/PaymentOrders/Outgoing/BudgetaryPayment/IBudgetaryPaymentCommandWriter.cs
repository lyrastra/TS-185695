using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.BudgetaryPayment.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportBudgetaryPayment commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateBudgetaryPayment commandData);
    }
}