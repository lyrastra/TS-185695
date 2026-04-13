using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalaryProject;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface ISalaryProjectApiClient
    {
        Task<SalaryProjectDto> GetSalaryProject(FirmId firmId, UserId userId);

        Task<SalaryProjectDto> GetSalaryProjectByDocumentBaseId(FirmId firmId, UserId userId, long documentBaseId);
    }
}