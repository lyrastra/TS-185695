using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.InvoicePaymentOrder;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore
{
    public interface IInvoicePaymentOrderApiClient
    {
        /// <summary> Выполняет синхронизацию данных, включая сравнение текущих деталей и их обновление при необходимости для сквозного(прямого) платежа</summary>
        /// <param name="requestDto"> Данные для обновления </param>
        /// <param name="ct"> Токен отмены </param>
        /// <returns> Состояние обновления </returns>
        Task<bool> SyncDetailAsync(InvoiceSyncDetailRequestDto requestDto, CancellationToken ct);
    }
}