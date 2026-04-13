using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

public class SetMlSkipStatusDto
{
    /// <summary>
    /// Идентификатор записи о верификации типа операции
    /// </summary>
    public long VerifyId { get; set; }

    /// <summary>
    /// Пропускам с массовой страницы или нет
    /// </summary>
    public SkipByMlStatus Status { get; set; }
}
