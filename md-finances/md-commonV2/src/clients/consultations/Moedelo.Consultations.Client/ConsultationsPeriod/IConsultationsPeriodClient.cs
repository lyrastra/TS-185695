using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Consultations.Dto.ConsultationsPeriod;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Consultations.Client.ConsultationsPeriod
{
    public interface IConsultationsPeriodClient : IDI
    {
        Task SetAllPeriodsAsync(BasePeriodsRequestDto request);

        Task SetPeriodsAsync(SetPeriodsRequestDto request);

        Task<List<ConsultationsPeriodDto>> GetAllPeriodsAsync();
    }
}