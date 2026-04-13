using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

/// <summary>
/// Данные для продления продуктовой услуги на маркетплейсе 
/// </summary>
public class PackageProlongationInfoDto
{
    /// <summary>
    /// Флаг доступности продления
    /// </summary>
    public bool IsProlongationAvailable { get;set; }

    /// <summary>
    /// Флаг соответствия модификаторов купленного пакета актуальной структуре ПУ (информирует о необходимости редактирования пакета)
    /// </summary>
    public bool IsPaidConfigMatchesToPackage { get; set; }

    /// <summary>
    /// Информация о пакете
    /// </summary>
    public MarketplacePackageDto Package { get; set; }

    /// <summary>
    /// Текущий срок действия (длительности)
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Доступен на странице "Тарифы"
    /// </summary>
    public bool AvailableOnPackagesTab { get; set; }

    /// <summary>
    /// Дата начала действия 
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Сообщение о продлении с 1-го числа текущего отчетного квартала
    /// </summary>
    public string ProlongationAtQuarterBeginMessage { get; set; }

    /// <summary>
    /// Стоимость продления услуги 
    /// </summary>
    public PackageCostResponseDto ProlongationServiceCost { get; set; }
    
    /// <summary>
    /// Статус доступности продления
    /// </summary>
    public ProlongationAvailabilityStatus Status { get; set; }
    
    /// <summary>
    /// Данные продления аутсорсинга
    /// </summary>
    public AdditionalOutsourcingProlongationDataDto OutsourcingProlongationData { get; set; }
}