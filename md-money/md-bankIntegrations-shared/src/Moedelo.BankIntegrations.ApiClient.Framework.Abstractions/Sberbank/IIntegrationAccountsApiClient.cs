using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.AccountRequest;
using Moedelo.BankIntegrations.ApiClient.Dto.Sberbank;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations
{
    public interface IIntegrationAccountsApiClient
    {
        Task ScheduleAccountsAndMovementsFetchAsync(AccountRequestDto dto);
        Task ScheduleFillingRequisitesAsync(FillingByInnRequestDto dto);
    }
}