using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CashV2.Dto.Cashbox;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.CashV2.Client.Contracts
{
    public interface ICashboxApiClient : IDI
    {
        Task<List<CashboxDto>> GetList(int firmId);
    }
}