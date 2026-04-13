using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto;
using Moedelo.ErptV2.Dto.EReportRequisites;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.EReportRequisites
{
    public interface IEReportRequisitesApiClient : IDI
    {
        Task<EReportRequisitesDto> Get(int firmId);

        Task<List<FnsData>> GetAdditionalFnsAsync(int firmId);

        Task<BaseDto> AddAdditionalFnsAsync(AdditionalFnsData fnsData);
        Task ResetAdditionalFnsAsync(int firmId);

        Task DeleteAdditionalFnsAsync(int firmId, string tax, string kpp);
    }
}