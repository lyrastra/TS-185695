using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.Utm;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BackofficeV2.Client.Utm
{
    public interface IUtmClient : IDI
    {
        Task<List<UtmTermDto>> GetUtmTermsByKeysAsync(List<string> keys);
    }
}