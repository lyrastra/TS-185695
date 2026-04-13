using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsCommandWriter
    {
        Task WriteImportAsync(ImportPaymentToNaturalPersons commandData);
        
        Task WriteImportDuplicateAsync(ImportDuplicatePaymentToNaturalPersons commandData);
        
        Task WriteImportWithMissingEmployeeAsync(ImportWithMissingEmployeePaymentToNaturalPersons commandData);
    }
}