using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface ISalaryPaymentsApiClient
    {
        Task<DocumentsNumbersDto> GetPaymentDocumentLastNumbersAsync(int firmId, int userId, GetLastDocumentsNumbersRequestDto request);

        Task<SavedPaymentsDocumentResultDto> SavePaymentDocumentsAsync(int firmId, int userId,
            SavingPaymentsModelDto data);

        Task<bool> HasDependenciesByWorkerAsync(int firmId, int userId, int workerId);
    }
}