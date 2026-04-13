using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Receipts.Dto;

namespace Moedelo.Billing.Abstractions.Receipts;

public interface IReceiptsApiClient
{
    Task SendAsync(ReceiptSendRequestDto requestDto);
}