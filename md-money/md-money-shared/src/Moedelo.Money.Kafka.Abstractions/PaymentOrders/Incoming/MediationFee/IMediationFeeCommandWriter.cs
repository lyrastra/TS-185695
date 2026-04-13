using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeCommandWriter
    {
        Task WriteImportAsync(ImportMediationFee commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateMediationFee commandData);

        Task WriteImportWithMissingContractAsync(ImportWithMissingContractMediationFee commandData);

        Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorMediationFee commandData);
    }
}