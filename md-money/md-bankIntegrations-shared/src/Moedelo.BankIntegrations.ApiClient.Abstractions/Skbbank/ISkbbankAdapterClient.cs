using System.Collections.Generic;
using Moedelo.BankIntegrations.ApiClient.Dto.Skbbank;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Skbbank
{
    public interface ISkbbankAdapterClient
    {

        Task<UserInfoDto> GetUserInfo(string code);

        Task<SkbMovementRequestResponseDto> InitRequestMovement(string accountNumber, int firmId);

        Task<string> GetServId(int firmId);

        Task<Dictionary<int, string>> TransferToken();
    }
}
