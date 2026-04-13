using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.RequisitesV2.Dto.Phones;

namespace Moedelo.RequisitesV2.Client.Phones
{
    public interface IPhoneClient : IDI
    {
        Task<List<PhoneDto>> GetAllAsync(int firmId);
    }
}