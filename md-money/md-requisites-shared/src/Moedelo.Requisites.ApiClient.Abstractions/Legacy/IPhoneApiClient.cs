using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IPhoneApiClient
    {
        Task<IReadOnlyCollection<PhoneDto>> GetAllAsync(int firmId);
    }
}