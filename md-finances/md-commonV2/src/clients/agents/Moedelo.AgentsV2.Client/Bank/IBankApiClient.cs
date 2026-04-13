using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.Bank;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.Bank
{
    public interface IBankApiClient : IDI
    {
        Task<List<BankItemDto>> GetBanks(string query, int count);
    }
}
