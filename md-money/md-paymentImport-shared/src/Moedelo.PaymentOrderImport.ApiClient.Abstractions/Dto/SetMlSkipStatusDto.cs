using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

public class SetMlSkipStatusDto
{
    /// <summary>
    /// Идентификатор базового докумена платежа по р/сч
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Идентификатор импорта
    /// </summary>
    public int ImportLogId { get; set; }

    /// <summary>
    /// Пропускам с массовой страницы или нет
    /// </summary>
    public SkipByMlStatus Status { get; set; }
}
