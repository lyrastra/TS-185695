using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FirmRequisites;

namespace Moedelo.RequisitesV2.Client.FirmRequisites
{
    public interface IFirmRequisitiesAnalyticClient : IDI
    {
        Task<List<FirmRequisitiesAnalyticDto>> GetFirmsRequisitiesAsync(List<int> firmIds);
    }
}