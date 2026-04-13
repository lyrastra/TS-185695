using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PaymentDocuments;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.PaymentDocuments
{
    public interface IPaymentDocumentsApiClient : IDI
    {
        Task<IncomingOutgoingSumDto> GetKontragentIncomingAndOutgoingOperationsSumAsync(int firmId, int userId, int kontragentId);

        Task<List<IncomingOutgoingSumDto>> GetKontragentIncomingAndOutgoingOperationsSumListAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);

        Task ReplaceKontragentInPaymentDocumentsAsync(int firmId, int userId, KontragentReplaceDto request);
    }
}
