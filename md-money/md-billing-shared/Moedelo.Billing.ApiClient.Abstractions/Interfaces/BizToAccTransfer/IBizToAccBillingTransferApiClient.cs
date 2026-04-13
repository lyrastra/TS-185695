using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.SwitchOn;

namespace Moedelo.Billing.Abstractions.Interfaces.BizToAccTransfer;

/// <summary>
/// Перевод БИЗ - УС: перенос биллинга
/// </summary>
public interface IBizToAccBillingTransferApiClient
{
    /// <summary>
    /// Валидирует возможность перевода биллинга
    /// </summary>
    Task<TransferValidation.ResultDto> ValidateAsync(int fromFirmId);

    /// <summary>
    /// Возвращает контекст перевода (платежи для перевода, текущий платеж и т. п.)
    /// </summary>
    Task<ContextDto> GetContextAsync(int fromFirmId);

    /// <summary>
    /// Переносит платеж (перепривязывает транзакции, при необходимости выставляет счет)
    /// </summary>
    /// <returns>соответствие старого платежа новому</returns>
    Task<TransferPayment.ResultDto> TransferPaymentAsync(TransferPayment.RequestDto dto);

    /// <summary>
    /// Откатывает перенос платежа
    /// </summary>
    Task RollbackPaymentTransactionsAsync(RollbackPaymentTransactionsDto dto);

    /// <summary>
    /// Устанавливает существующий платеж "ПРОСРОЧЕННЫМ ТЕХНИЧЕСКИМ" (для доступа в ЛК)
    /// </summary>
    Task<ExpiredAccessPayment.ResultDto> SetExpiredAccessPaymentAsync(int paymentId);

    /// <summary>
    /// Откатывает установку платежа "ПРОСРОЧЕННЫМ ТЕХНИЧЕСКИМ"
    /// </summary>
    Task RollbackSetExpiredAccessPaymentAsync(ExpiredAccessPayment.ResultDto dto);

    /// <summary>
    /// Включение переведённого платежа из НБ
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<TransferedPaymentMapDto> SwithOnTransferredPaymentAsync(SwitchOnTransferredPaymentRequestDto dto);

    /// <summary>
    /// Устанавливает существующий платеж "ТЕХНИЧЕСКИМ" (для доступа в ЛК) с указанной датой окончания
    /// </summary>
    Task<Payment.ResultDto> SetPaymentAsync(Payment.RequestDto dto);

    /// <summary>
    /// Откатывает установку платежа "ТЕХНИЧЕСКИМ"
    /// </summary>
    Task RollbackSetPaymentAsync(Payment.ResultDto dto);
}