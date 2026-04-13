using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.ChargeSettings;

namespace Moedelo.PayrollV2.Client.ChargeSettings
{
    public interface IChargeSettingsApiClient: IDI
    {
        Task<List<SalaryTemplateDto>> GetSalaryTemplatesAsync(int firmId, int userId, SalaryTemplatesRequestDto request);
    }
}