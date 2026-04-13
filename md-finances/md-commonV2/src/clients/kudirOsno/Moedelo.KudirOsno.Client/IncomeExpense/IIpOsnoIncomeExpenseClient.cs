using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KudirOsno.Client.IncomeExpense.Dto;
using System.Threading.Tasks;

namespace Moedelo.KudirOsno.Client.IncomeExpense
{
    public interface IIpOsnoIncomeExpenseClient : IDI
    {
        Task<IncomeExpenseResponseDto[]> GetAsync(int firmId, int userId, int year, int[] quarters);
    }
}
