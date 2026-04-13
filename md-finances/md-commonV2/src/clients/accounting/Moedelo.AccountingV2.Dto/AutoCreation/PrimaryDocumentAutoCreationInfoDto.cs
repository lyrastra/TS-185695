using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.AutoCreation;

public class PrimaryDocumentAutoCreationInfoDto
{
    /// <summary>
    /// Для документа настроено создание по расписанию
    /// </summary>
    public bool IsAutoCopyOrigin { get; set; }

    /// <summary>
    /// Документ создан по расписанию
    /// </summary>
    public bool HasCreatedBySchedule { get; set; }

    /// <summary>
    /// BaseId документа-прототипа. Если данный документ уже удалён, то содержит 0.
    /// </summary>
    public long? OriginDocId { get; set; }

    /// <summary>
    /// Тип документа-прототипа (актуален в случае настройки "создавать акт на основании счета")
    /// </summary>
    public AccountingDocumentType? OriginDocType { get; set; }
}