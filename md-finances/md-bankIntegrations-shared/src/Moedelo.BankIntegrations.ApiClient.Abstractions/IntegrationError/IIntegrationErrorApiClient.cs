using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationError
{
    public interface IIntegrationErrorApiClient
    {
        /// <summary>Прочитать непрочитанные ошибочные уведомления по партнеру</summary>
        /// <param name="dto"> Параметры </param>
        Task ReadUnreadByPartnerAsync(ReadUnreadIntegrationErrorRequestDto dto);

        /// <summary>Сохранить ошибку, возникшую при запросе выписки</summary>
        /// <param name="dto">Параметры ошибки</param>
        Task SaveAsync(IntegrationErrorSaveRequestDto dto, CancellationToken cancellationToken = default);
    }
}