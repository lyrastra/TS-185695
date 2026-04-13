using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders
{
    /// <summary> Перенести операции из зелёной зоны в обычную зону </summary>
    public interface IPaymentOrderOperationApiClient
    {
        Task ApproveImportedAsync(ApproveImportedOperationsRequestDto dto);
    }
}