using Moedelo.CashV2.Client.Contracts;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Sources.Readers
{
    [InjectAsSingleton]
    public class YandexKassaDataIntegrationDataReader : IYandexKassaIntegrationDataReader
    {
        private readonly IYandexKassaApiClient yandexKassaApiClient;

        public YandexKassaDataIntegrationDataReader(
            IYandexKassaApiClient yandexKassaApiClient)
        {
            this.yandexKassaApiClient = yandexKassaApiClient;
        }

        public async Task<IntegrationData> GetData(int firmId, int userId)
        {
            var yandexKassaIntegration = await yandexKassaApiClient.GetYandexKassaIntegrationStatusAsync(firmId, userId).ConfigureAwait(false);
            if (yandexKassaIntegration.IsRequestForIntegrationSent)
            {
                return new IntegrationData
                {
                    IntegrationPartner = IntegrationPartners.YandexKassa,
                    HasActiveIntegration = true,
                    CanRequestMovementList = true
                };
            }

            return null;
        }
    }
}
