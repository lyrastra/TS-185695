using Moedelo.AccountingV2.Client.ReturnToCustomer.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.ReturnToCustomer
{
    public interface IReturnToCustomerApiClient : IDI
    {
        Task<long> CreateOrUpdateAsync(int firmId, int userId, ReturnToCustomerDto dto);
        Task<ReturnToCustomerDto> GetOrderByBaseIdAsync(int firmId, int userId, long id);
        Task DeleteAsync(int firmId, int userId, long id);

    }
}
