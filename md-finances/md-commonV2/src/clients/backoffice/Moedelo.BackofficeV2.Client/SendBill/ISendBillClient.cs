using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.SendBill;

namespace Moedelo.BackofficeV2.Client.SendBill
{
    public interface ISendBillClient
    {
        Task<List<SendBillResponseDto>> SendAsync(List<SendBillRequestDto> sendBillDtos);

        Task<string> ResendBillAsync(ResendBillRequestDto dto);
    }
}