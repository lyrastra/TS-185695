using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.YooCash.Dto;

namespace Moedelo.YooCash.Client.Yookassa
{
    public interface IYookassaCreditApiClient : IDI
    {
        Task<YookassaCreditDto> GetAsync(int firmId, int userId, DateTime date);

        Task SetAsync(int firmId, int userId, YookassaCreditDto yookassaCredit);

        Task DeleteAsync(int firmId, int userId, DateTime date);
    }
}