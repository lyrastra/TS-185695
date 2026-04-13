using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KudirOsno.Client.Postings.Dto;
using System.Threading.Tasks;

namespace Moedelo.KudirOsno.Client.TaxRemains
{
    public interface IIpOsnoTaxRemainsClient : IDI
    {
        Task<TaxRemainsDto> GetAsync(int firmId, int userId);
        Task SaveAsync(int firmId, int userId, TaxRemainsDto dto);
        Task DeleteAsync(int firmId, int userId);
    }
}