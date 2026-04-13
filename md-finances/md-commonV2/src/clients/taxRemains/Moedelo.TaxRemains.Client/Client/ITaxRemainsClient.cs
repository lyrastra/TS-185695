using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TaxRemains.Client.Dto;

namespace Moedelo.TaxRemains.Client.Client
{
    public interface ITaxRemainsClient : IDI
    {
        Task<TaxRemainDto> GetAsync(int firmId);
    }
}
