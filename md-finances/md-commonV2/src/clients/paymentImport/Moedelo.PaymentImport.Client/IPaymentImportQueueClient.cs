using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;
using System.Threading.Tasks;

namespace Moedelo.PaymentImport.Client
{
    public interface IPaymentImportQueueClient : IDI
    {
        Task AppendReconcilationEventAsync(ReconciliationEventAppendRequestDto dto);
    }
}