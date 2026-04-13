using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PaymentDocuments;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.PrimaryDocuments
{
    public interface IPrimaryDocumentsOldApiClient : IDI
    {
        Task<DocumentsSumsDto> GetDocumentSumsAsync(int firmId, int userId, int kontragentId, PrimaryDocumentsTransferDirection direction);
        Task<List<KontragentDocumentsSumsAndDirectionDto>> GetOverallDocumentSumsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);
        Task<List<DocumentsCountByKontragentDto>> GetDocumentCountsByKontragentsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);
        Task<FakeDocumentsDto> GetFakeDocumentsCountAsync(int firmId, int userId);
        Task<List<FakeDocumentDto>> GetFakeDocumentsAsync(int firmId, int userId, AccountingDocumentType documentType);
        Task<BillResponseDto> ChangeStatusByIdAndSumAsync(int firmId, int userId, BillChangeStatusQueryParamsDto dto);
        Task<List<BillShortInfoDto>> GetByNamesAsync(int firmId, int userId, IList<string> names);
        
        Task<ListDto<PaymentDocumentLinkDto>> GetPaymentDocumentLinksAsync(int firmId, int userId, PaymentDocumentLinkRequestDto requestDto);
        Task UpdateConfirmingDocumentsAsync(int firmId, int userId, ConfirmingDocumentsDto dto);
        Task UpdateConfirmingLinkedDocumentsAsync(int firmId, int userId, ConfirmingLinkedDocumentsDto dto);
        Task DeleteConfirmingDocumentsAsync(int firmId, int userId, int primaryDocumentId, AccountingDocumentType documentType);
    }
}
