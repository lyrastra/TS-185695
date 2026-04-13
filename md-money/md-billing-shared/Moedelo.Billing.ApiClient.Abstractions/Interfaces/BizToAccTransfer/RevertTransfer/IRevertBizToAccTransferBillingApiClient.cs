using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.Context;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.State;

namespace Moedelo.Billing.Abstractions.Interfaces.BizToAccTransfer.RevertTransfer;

public interface IRevertBizToAccTransferBillingApiClient
{
    Task<RevertTransferContextDto> GetRevertContextAsync(RevertTransferContextRequestDto dto);

    Task<RevertTransferPaymentContextDto> GetRevertTransferPaymentContextAsync(int paymentId);

    Task<SwitchOffAccPaymentStateDto> SwitchOffAccPaymentAsync(int accPaymentId);

    Task<SwitchOnBizPaymentStateDto> SwitchOnBizPaymentAsync(SwitchOnBizPaymentRequestDto requestDto);

    Task RollbackSwitchOffAccPaymentAsync(SwitchOffAccPaymentStateDto prevState);

    Task RollbackSwitchOnBizPaymentAsync(SwitchOnBizPaymentStateDto prevState);
}