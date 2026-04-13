using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TaxRemains.Client.Dto;

namespace Moedelo.TaxRemains.Client.Client
{
    public interface IFixedPaymentRemainsClient : IDI
    {
        Task<FixedPaymentRemainsDto> GetAsync(int firmId);
    }
}
