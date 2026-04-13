using System.Threading.Tasks;
using Moedelo.Address.Dto.House;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Address.Client.House
{
    public interface IHouseApiClient : IDI
    {
        Task<HouseDto> GetByAoGuidAndNumber(HouseGetByRequestDto dto);
    }
}