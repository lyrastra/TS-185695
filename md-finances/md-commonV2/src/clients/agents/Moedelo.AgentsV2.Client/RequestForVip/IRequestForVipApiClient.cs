using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.Enums;
using Moedelo.AgentsV2.Dto.Partners;
using Moedelo.AgentsV2.Dto.RequestForVip;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.RequestForVip
{
    public interface IRequestForVipApiClient : IDI
    {
        Task<ResponseStatusCode> RemoveRequestForVip(PartnerIdDto partnerIdDto);

        Task<ResponseStatusCode> ApproveRequestForVip(ApproveRequestForVipDto approveRequestForVipDto);

        Task<int> GetNotApprovedVipRequestCount();

        Task<List<RequestForVipDto>> GetNotApprovedVipRequests(int page, int pageSize);
    }
}
