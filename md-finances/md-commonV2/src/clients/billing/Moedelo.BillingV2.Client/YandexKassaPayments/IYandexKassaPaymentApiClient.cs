using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.YandexKassaPayments;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.YandexKassaPayments
{
    public interface IYandexKassaPaymentApiClient : IDI
    {
        Task SaveYandexKassaPaymentAsync(YandexKassaPaymentDto dto);
        Task<List<YandexKassaPaymentDto>> GetYandexKassaPaymentsAsync(GetYandexKassaPaymentsCriteriaRequest request);
    }
}
