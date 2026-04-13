using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.FixedPayments;

namespace Moedelo.RptV2.Client.FixedPayments
{
    public interface IFixedPaymentsApiClient
    {
        Task<FileResponseDto> GetRegistryAsync(int userId, int firmId);
        Task<List<FixedPaymentsWizardDataDto>> GetCompletedWizardDataByYearAsync(int userId, int firmId, int year);
    }
}
