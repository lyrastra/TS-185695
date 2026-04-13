using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.AccountBanner;
using Moedelo.Common.Enums.Enums.Account;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.AccountBanner
{
    public interface IAccountBannerApiClient : IDI
    {
        Task<List<AccountBannerDto>> GetByAccountTypeAsync(AccountType accountType, int firmId, int userId);

        Task<List<AccountBannerDto>> GetAllAsync(int firmId, int userId);

        Task<AccountBannerDto> GetByIdAsync(long id, int firmId, int userId);

        Task<long> SaveAsync(AccountBannerDto accountBannerDto, int firmId, int userId);

        Task Delete(long id, int firmId, int userId);
    }
}