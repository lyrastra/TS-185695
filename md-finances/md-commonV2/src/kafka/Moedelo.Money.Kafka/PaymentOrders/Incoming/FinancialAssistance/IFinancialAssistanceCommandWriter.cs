using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.FinancialAssistance.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.FinancialAssistance
{
    public interface IFinancialAssistanceCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportFinancialAssistance commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateFinancialAssistance commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorFinancialAssistance commandData);
    }
}