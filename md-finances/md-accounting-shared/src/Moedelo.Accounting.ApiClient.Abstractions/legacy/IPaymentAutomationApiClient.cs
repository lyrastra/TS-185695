using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IPaymentAutomationApiClient
    {
        Task<ReasonDocumentDto[]> GetReasonDocumentsAsync(FirmId firmId, UserId userId, FindReasonDocumentsAutomationDto dto);
    }
}