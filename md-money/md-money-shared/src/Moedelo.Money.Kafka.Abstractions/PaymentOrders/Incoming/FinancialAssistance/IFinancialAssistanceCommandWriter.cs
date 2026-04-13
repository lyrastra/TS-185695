using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.FinancialAssistance.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.FinancialAssistance
{
    public interface IFinancialAssistanceCommandWriter
    {
        Task WriteImportAsync(
            ImportFinancialAssistance commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateFinancialAssistance commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingContractorFinancialAssistance commandData);
    }
}