using Moedelo.AccountingV2.Client.TransferRetailRevenue.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.TransferRetailRevenue
{
    public interface ITransferRetailRevenueClient : IDI
    {
        Task<long> CreateAsync(int firmId, int userId, AccountingCashOrderDto dto);
    }
}
