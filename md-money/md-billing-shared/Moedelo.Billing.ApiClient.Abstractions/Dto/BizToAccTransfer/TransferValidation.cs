using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;

/// <summary>
/// БИЗ - УС: валидация биллинга на возможность переноса
/// </summary>
public class TransferValidation
{
    public class RequestDto
    {
        /// <summary>
        /// Идентификатор фирмы, которую планируется перевести
        /// </summary>
        public int FirmId { get; set; }
    }

    public class ResultDto
    {
        /// <summary>
        /// Ошибка валидации (если null, валидация пройдена успешно)
        /// </summary>
        public BizToAccValidationFailedReason? FailReason { get; set; }

        /// <summary>
        /// Идентификатор невалидного платежа (в случае большинства ошибок валидации)
        /// </summary>
        public int? InvalidPaymentId { get; set; }
    }
}