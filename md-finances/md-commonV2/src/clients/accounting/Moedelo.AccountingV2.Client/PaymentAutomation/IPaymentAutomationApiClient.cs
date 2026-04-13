using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.PaymentAutomation
{
    public interface IPaymentAutomationApiClient : IDI
    {
        Task<List<PaymentReasonDocumentDto>> GetReasonDocumentsAsync(int firmId, int userId, ReasonDocumentsAutomationDto dto);
    }
}
