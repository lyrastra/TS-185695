using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract
{
    public interface IAgencyContractCommandWriter
    {
        Task WriteImportAsync(ImportAgencyContract commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateAgencyContract commandData);

        Task WriteImportWithMissingContractAsync(ImportWithMissingContractAgencyContract commandData);

        Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorAgencyContract commandData);
    }
}