using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.Involvement;
using Moedelo.SuiteCrm.Dto.Marketing;

namespace Moedelo.SuiteCrm.Client
{
    public interface ISuiteCrmRequestsApiClient : IDI
    {
        Task ThankYouBuroLandingRequestAsync(ThankYouLandingDto data);
        
        Task ThankYouBizLandingRequestAsync(ThankYouLandingDto data);

        Task TaskFromKayakoRequestAsync(TaskFromKayakoDto data);

        Task UpdateLeadForInvolvementAsync(InvolvementInfoDto dto);
    }
}