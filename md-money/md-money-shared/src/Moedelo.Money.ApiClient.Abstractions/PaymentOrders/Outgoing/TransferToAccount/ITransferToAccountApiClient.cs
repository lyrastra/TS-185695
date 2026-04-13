using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    /// <summary> Перевести на другой р/с </summary>
    public interface ITransferToAccountApiClient
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(TransferToAccountSaveDto createRequest);
    }
}
