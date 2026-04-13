using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CashV2.Dto.YandexKassa;
using Moedelo.CashV2.Dto.YandexKassa.Integration;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.CashV2.Client.Contracts
{
    public interface IYandexKassaApiClient : IDI
    {
        Task<bool> UpdatePreferedTaxationSystem(int firmId, long shopId, TaxationSystemType taxationSystem);

        Task<GetAndSaveBankExtractsDto> UpdateIntegrationsInfo(int firmId, int userId, IntegrationsRequestDto integrations);

        Task<YandexKassaIntegrationResponseDto> SendShopAccessGrantEmailAsync(int firmId, int userId, YandexKassaIntegrationRequestDto yandexKassaIntegrationRequest);

        Task<bool> TurnYandexKassaShopIntegrationAsync(int firmId, int userId, YandexKassaShopIntegrationTurnClientData yandexKassaShop);

        Task<YandexKassaAddShopResultDto> AddYandexKassaIntegratedShopAsync(int firmId, int userId, YandexKassaShopIntegrationTurnClientData yandexKassaShop);

        Task<bool> RemoveYandexKassaIntegratedShopAsync(int firmId, int userId, YandexKassaShopIntegrationTurnClientData yandexKassaShop);

        Task<List<YandexKassaShopClientData>> GetYandexKassaIntegratedShopListAsync(int firmId, int userId);

        Task<YandexKassaIntegrationStatusDto> GetYandexKassaIntegrationStatusAsync(int firmId, int userId);

        Task<YandexKassaRequestInfoDto> GetYandexKassaIntegrationRequestInfoAsync(int firmId, int userId);
    }
}