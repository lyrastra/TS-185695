using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BackofficeV2.Client.LeadChannel
{
    public interface ILeadChannelClient : IDI
    {
        Task RecalcChannelForFirmsAsync(IEnumerable<int> firmIds);
    }
}