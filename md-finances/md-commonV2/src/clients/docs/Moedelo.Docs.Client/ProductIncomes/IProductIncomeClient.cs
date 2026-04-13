using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.ProductIncomes
{
    public interface IProductIncomeClient : IDI
    {
        Task DeleteAsync(int firmId, int userId, long documentBaseId);
    }
}